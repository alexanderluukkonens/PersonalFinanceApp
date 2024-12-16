using Npgsql;

class Program
{
    public static bool isRunning = true;

    static async Task Main(string[] args)
    {
        string connectionString =
            "Host=localhost; Username=postgres; Password=password; Database=finance_app";
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        await DataBaseService.CreateDatabaseTable(connection);

        Console.Clear();
        IUserService userService = new PostgresUserService(connection);
        IMenuService menuService = new SimpleMenuService();
        ITransactionService transactionService = new PostgresTransactionService(connection, userService);
        LoginMenu loginMenu = new LoginMenu(userService, menuService, transactionService);
        menuService.SetMenu(loginMenu);

        while (isRunning)
        {
            menuService.GetMenu().Display();
            string inputCommand = Console.ReadLine()!;
            menuService.GetMenu().ExecuteCommand(inputCommand);
            Console.Clear();
        }
    }
}
