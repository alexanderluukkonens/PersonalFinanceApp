using Npgsql;
public class DatabaseService
{
    public static async Task CreateDatabaseTable(NpgsqlConnection connection)
    {
        try
        {
            var createTablesSql =
                @"
                CREATE TABLE IF NOT EXISTS users (
                    user_id UUID PRIMARY KEY,
                    name TEXT,
                    password TEXT
                );
                CREATE TABLE IF NOT EXISTS transactions (
                    transaction_id UUID PRIMARY KEY,
                    user_id UUID REFERENCES users (user_id),
                    amount DECIMAL,
                    date DATE,
                    description TEXT
                );
            ";
            using var createTableCmd = new NpgsqlCommand(createTablesSql, connection);
            await createTableCmd.ExecuteNonQueryAsync();
        }
        catch (PostgresException pgEx)
        {
            throw new Exception($"Database error while creating tables: {pgEx.Message}");
        }
        catch (NpgsqlException npgEx)
        {
            throw new Exception($"Connection error while creating tables: {npgEx.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected error while creating tables: {ex.Message}");
        }
    }
}
