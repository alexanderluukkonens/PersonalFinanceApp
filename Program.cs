using Npgsql;

class Program
{
    public static bool isRunning = true;

    static async Task Main(string[] args)
    {
        try
        {
            string connectionString = "Host=localhost; Username=postgres; Password=password; Database=finance_app";
            using var connection = new NpgsqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
            }
            catch (NpgsqlException)
            {
                Console.WriteLine("Error: Could not connect to the database. Please check if the database is running and the connection details are correct.");
                return;
            }

            try
            {
                await DatabaseService.CreateDatabaseTable(connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to create database tables: {ex.Message}");
                return;
            }

            Console.Clear();

            try
            {
                IUserService userService = new PostgresUserService(connection);
                IMenuService menuService = new SimpleMenuService();
                ITransactionService transactionService = new PostgresTransactionService(connection, userService);
                LoginMenu loginMenu = new LoginMenu(userService, menuService, transactionService);
                menuService.SetMenu(loginMenu);

                while (isRunning)
                {
                    try
                    {
                        menuService.GetMenu().Display();
                        string? inputCommand = Console.ReadLine();

                        if (inputCommand == null)
                        {
                            Console.WriteLine("Error: Invalid input. Please try again.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        }

                        menuService.GetMenu().ExecuteCommand(inputCommand);
                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occurred while processing command: {ex.Message}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error in application: {ex.Message}");
                Console.WriteLine("The application needs to close. Press any key to exit...");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
