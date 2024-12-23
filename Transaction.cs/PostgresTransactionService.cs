using Npgsql;

public class PostgresTransactionService : ITransactionService
{
    private NpgsqlConnection connection;

    private IUserService userService;

    public PostgresTransactionService(NpgsqlConnection connection, IUserService userService)
    {
        this.connection = connection;
        this.userService = userService;
    }

    public void AddTransaction(decimal amount, string description)
    {
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            throw new ArgumentException("You are not logged in.");
        }
        Transaction transaction = new Transaction
        {
            Transaction_id = Guid.NewGuid(),
            User_id = user.Id,
            Amount = amount,
            Date = DateTime.Now,
            Description = description,
        };

        var sql =
            @"INSERT INTO transactions (transaction_id, user_id, amount, date, description) VALUES
        (
            @transaction_id,
            @user_id,
            @amount,
            @date,
            @description

        )";

        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@transaction_id", transaction.Transaction_id);
        cmd.Parameters.AddWithValue("@user_id", transaction.User_id);
        cmd.Parameters.AddWithValue("@amount", transaction.Amount);
        cmd.Parameters.AddWithValue("@date", transaction.Date);
        cmd.Parameters.AddWithValue("@description", transaction.Description);

        cmd.ExecuteNonQuery();
        return;
    }

    public void DeleteTransaction(Guid transactionId)
    {
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            throw new InvalidOperationException("You must be logged in to delete transactions.");
        }

        // Börja en transaktion för att säkerställa dataintegritet
        using var transaction = connection.BeginTransaction();
        try
        {
            // Verifiera att transaktionen tillhör användaren igen som en extra säkerhetsåtgärd
            if (!VerifyTransactionOwnership(transactionId))
            {
                throw new InvalidOperationException(
                    "Transaction not found or you don't have permission to delete it."
                );
            }

            var sql =
                @"
                DELETE FROM transactions 
                WHERE transaction_id = @transaction_id 
                AND user_id = @user_id";

            using var cmd = new NpgsqlCommand(sql, connection, transaction);
            cmd.Parameters.AddWithValue("@transaction_id", transactionId);
            cmd.Parameters.AddWithValue("@user_id", user.Id);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Transaction not found or already deleted.");
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public string ShowCurrentBalance()
    {
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            throw new ArgumentException("You are not logged in.");
        }

        var sql =
            @"SELECT SUM(amount) 
            FROM transactions 
            INNER JOIN users ON transactions.user_id = users.user_id
            WHERE users.user_id = @user_id";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@user_id", user.Id);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }
        decimal amount = reader.GetDecimal(0);
        return $"{amount:C}";
    }

    public Transaction ShowTransactions()
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetTransactionsByTimespan(DateTime startDate, DateTime endDate)
    {
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            throw new ArgumentException("You are not logged in.");
        }

        var sql =
            @"
            SELECT transaction_id, user_id, amount, date, description
            FROM transactions
            WHERE user_id = @user_id 
            AND date >= @start_date 
            AND date <= @end_date
            ORDER BY date DESC";

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@user_id", user.Id);
        cmd.Parameters.AddWithValue("@start_date", startDate);
        cmd.Parameters.AddWithValue("@end_date", endDate);

        List<Transaction> transactions = new List<Transaction>();
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            transactions.Add(
                new Transaction
                {
                    Transaction_id = reader.GetGuid(0),
                    User_id = reader.GetGuid(1),
                    Amount = reader.GetDecimal(2),
                    Date = reader.GetDateTime(3),
                    Description = reader.GetString(4),
                }
            );
        }

        return transactions;
    }

    public List<Transaction> GetTransactionsByCustomDates(DateTime startDate, DateTime endDate)
    {
        return GetTransactionsByTimespan(startDate, endDate);
    }

    public bool VerifyTransactionOwnership(Guid transactionId)
    {
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            throw new InvalidOperationException(
                "You must be logged in to verify transaction ownership."
            );
        }

        var sql =
            @"
            SELECT COUNT(1) 
            FROM transactions 
            WHERE transaction_id = @transaction_id 
            AND user_id = @user_id";

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@transaction_id", transactionId);
        cmd.Parameters.AddWithValue("@user_id", user.Id);

        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }
}
