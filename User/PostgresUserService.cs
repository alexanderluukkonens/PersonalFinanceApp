using Npgsql;
using BCrypt.Net;
using System.Text;

public class PostgresUserService : IUserService
{
    private NpgsqlConnection connection;
    private Guid? loggedInUser = null;

    public PostgresUserService(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public User? GetLoggedInUser()
    {
        try
        {
            if (loggedInUser == null)
            {
                return null;
            }

            var sql = @"SELECT * FROM users WHERE user_id = @user_id";
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@user_id", loggedInUser);

            try
            {
                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                var user = new User
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Password = reader.GetString(2),
                };
                return user;
            }
            catch (PostgresException pgEx)
            {
                throw new Exception($"Database error while getting user: {pgEx.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get logged in user: {ex.Message}");
        }
    }

    public User? Login(string username, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var sql = @"SELECT * FROM users WHERE name = @username";
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    throw new InvalidOperationException("Invalid username or password.");
                }

                var storedHash = reader.GetString(2);
                if (!BCrypt.Net.BCrypt.Verify(password, storedHash))
                {
                    throw new InvalidOperationException("Invalid username or password.");
                }

                var user = new User
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Password = storedHash
                };
                loggedInUser = user.Id;
                return user;
            }
            catch (PostgresException pgEx)
            {
                throw new Exception($"Database error during login: {pgEx.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Login failed: {ex.Message}");
        }
    }

    public void Logout()
    {
        try
        {
            if (loggedInUser == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }
            loggedInUser = null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Logout failed: {ex.Message}");
        }
    }

    public User RegisterUser(string username, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var checkSql = @"SELECT COUNT(*) FROM users WHERE name = @username";
            using var checkCmd = new NpgsqlCommand(checkSql, this.connection);
            checkCmd.Parameters.AddWithValue("@username", username);

            try
            {
                int existingUsers = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (existingUsers > 0)
                {
                    throw new InvalidOperationException("Username already exists.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = username,
                    Password = hashedPassword,
                };

                var sql = @"INSERT INTO users (user_id, name, password) 
                          VALUES (@id, @name, @password)";
                using var cmd = new NpgsqlCommand(sql, this.connection);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@password", user.Password);

                cmd.ExecuteNonQuery();
                return user;
            }
            catch (PostgresException pgEx)
            {
                throw new Exception($"Database error during registration: {pgEx.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Registration failed: {ex.Message}");
        }
    }

    public string HandlePasswordInput()
    {
        try
        {
            var password = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Length--;
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occurred while reading password.");
            Console.WriteLine($"Error details: {ex.Message}");
            return string.Empty;
        }
    }
}