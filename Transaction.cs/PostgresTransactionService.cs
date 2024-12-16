using Npgsql;

public class PostgresTransactionService : ITransactionService
{
    private NpgsqlConnection connection;

    private IUserService userService;

    private List<Transaction> transactions = new List<Transaction>();

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
            throw new ArgumentException("You are not logged in.");
        }
        var deleteSql = "DELETE FROM transactions WHERE transaction_id = @transaction_id";
        using var deleteCmd = new NpgsqlCommand(deleteSql, this.connection);
        deleteCmd.Parameters.AddWithValue("@transaction_id", transactionId);
        deleteCmd.ExecuteNonQuery();
    }

    public decimal? ShowCurrentBalance()
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
        return amount;
    }

    public Transaction ShowTransactions()
    {
        throw new NotImplementedException();
    }
}
