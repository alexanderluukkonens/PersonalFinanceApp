using System.Security.Cryptography.X509Certificates;
using Npgsql;

namespace IndividuellUppgiftDatabaser;

class Program
{
    public static bool isRunning = true;

    static async Task Main(string[] args)
    {
        string connectionString =
            "Host=localhost; Username=postgres; Password=Hej123; Database=finance_app";
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        await DataBaseService.CreateDatabaseTable(connection);

        IUserService userService = new PostgresUserService(connection);
        IMenuService menuService = new SimpleMenuService();
        LoginMenu loginMenu = new LoginMenu(userService);
        menuService.SetMenu(loginMenu);

        while (isRunning)
        {
            string inputCommand = Console.ReadLine()!;
            menuService.GetMenu().ExecuteCommand(inputCommand);
        }
    }
}
