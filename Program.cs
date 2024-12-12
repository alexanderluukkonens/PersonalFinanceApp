using System.Security.Cryptography.X509Certificates;
using Npgsql;

namespace IndividuellUppgiftDatabaser;

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
        LoginMenu loginMenu = new LoginMenu(userService, menuService);
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
