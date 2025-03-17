using System.Data;
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
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Transaction description cannot be empty.");
            }

            Transaction transaction = new Transaction
            {
                Transaction_id = Guid.NewGuid(),
                User_id = user.Id,
                Amount = amount,
                Date = DateTime.Now,
                Description = description,
            };

            using var dbTransaction = connection.BeginTransaction();
            try
            {
                var sql = @"INSERT INTO transactions (transaction_id, user_id, amount, date, description) 
                           VALUES (@transaction_id, @user_id, @amount, @date, @description)";

                using var cmd = new NpgsqlCommand(sql, connection, dbTransaction);
                cmd.Parameters.AddWithValue("@transaction_id", transaction.Transaction_id);
                cmd.Parameters.AddWithValue("@user_id", transaction.User_id);
                cmd.Parameters.AddWithValue("@amount", transaction.Amount);
                cmd.Parameters.AddWithValue("@date", transaction.Date);
                cmd.Parameters.AddWithValue("@description", transaction.Description);

                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            catch (PostgresException pgEx)
            {
                dbTransaction.Rollback();
                throw new Exception($"Database error while adding transaction: {pgEx.Message}");
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw new Exception($"Error while adding transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add transaction: {ex.Message}");
        }
    }

    public void DeleteTransaction(Guid transactionId)
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new InvalidOperationException("You must be logged in to delete transactions.");
            }

            if (transactionId == Guid.Empty)
            {
                throw new ArgumentException("Invalid transaction ID.");
            }

            using var dbTransaction = connection.BeginTransaction();
            try
            {
                if (!VerifyTransactionOwnership(transactionId))
                {
                    throw new InvalidOperationException("Transaction not found or you don't have permission to delete it.");
                }

                var sql = @"
                    DELETE FROM transactions 
                    WHERE transaction_id = @transaction_id 
                    AND user_id = @user_id";

                using var cmd = new NpgsqlCommand(sql, connection, dbTransaction);
                cmd.Parameters.AddWithValue("@transaction_id", transactionId);
                cmd.Parameters.AddWithValue("@user_id", user.Id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Transaction not found or already deleted.");
                }

                dbTransaction.Commit();
            }
            catch (PostgresException pgEx)
            {
                dbTransaction.Rollback();
                throw new Exception($"Database error while deleting transaction: {pgEx.Message}");
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw new Exception($"Error while deleting transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete transaction: {ex.Message}");
        }
    }

    public string ShowCurrentBalance()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            using var dbTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var sql = @"
                    SELECT SUM(amount)
                    FROM transactions
                    INNER JOIN users ON transactions.user_id = users.user_id
                    WHERE users.user_id = @user_id";

                using var cmd = new NpgsqlCommand(sql, connection, dbTransaction);
                cmd.Parameters.AddWithValue("@user_id", user.Id);

                decimal amount;
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return "0.00 kr";
                    }
                    amount = reader.IsDBNull(0) ? 0M : reader.GetDecimal(0);
                }

                dbTransaction.Commit();
                return $"{amount:C}";
            }
            catch (PostgresException pgEx)
            {
                dbTransaction.Rollback();
                throw new Exception($"Database error while retrieving balance: {pgEx.Message}");
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw new Exception($"Error while retrieving balance: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to show current balance: {ex.Message}");
        }
    }

    public List<Transaction> GetTransactionsByTimespan(DateTime startDate, DateTime endDate)
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be after end date.");
            }

            var sql = @"
                SELECT t.transaction_id, t.user_id, t.amount, t.date, t.description
                FROM transactions t
                INNER JOIN users u ON t.user_id = u.user_id
                WHERE t.user_id = @user_id 
                AND t.date >= @start_date 
                AND t.date <= @end_date
                ORDER BY t.date DESC";

            using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@user_id", user.Id);
            cmd.Parameters.AddWithValue("@start_date", startDate);
            cmd.Parameters.AddWithValue("@end_date", endDate);

            List<Transaction> transactions = new List<Transaction>();

            try
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        Transaction_id = reader.GetGuid(0),
                        User_id = reader.GetGuid(1),
                        Amount = reader.GetDecimal(2),
                        Date = reader.GetDateTime(3),
                        Description = reader.GetString(4),
                    });
                }
            }
            catch (PostgresException pgEx)
            {
                throw new Exception($"Database error while retrieving transactions: {pgEx.Message}");
            }

            return transactions;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve transactions: {ex.Message}");
        }
    }

    public List<Transaction> GetTransactionsByCustomDates(DateTime startDate, DateTime endDate)
    {
        return GetTransactionsByTimespan(startDate, endDate);
    }

    public bool VerifyTransactionOwnership(Guid transactionId)
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new InvalidOperationException("You must be logged in to verify transaction ownership.");
            }

            if (transactionId == Guid.Empty)
            {
                throw new ArgumentException("Invalid transaction ID.");
            }

            var sql = @"
                SELECT COUNT(1) 
                FROM transactions t
                INNER JOIN users u ON t.user_id = u.user_id
                WHERE t.transaction_id = @transaction_id 
                AND t.user_id = @user_id";

            using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@transaction_id", transactionId);
            cmd.Parameters.AddWithValue("@user_id", user.Id);

            try
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (PostgresException pgEx)
            {
                throw new Exception($"Database error while verifying transaction ownership: {pgEx.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to verify transaction ownership: {ex.Message}");
        }
    }
}