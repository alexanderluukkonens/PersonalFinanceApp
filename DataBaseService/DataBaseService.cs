using Npgsql;

namespace IndividuellUppgiftDatabaser;

public class DataBaseService
{
    public static async Task CreateDatabaseTable(NpgsqlConnection connection)
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
}
