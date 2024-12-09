using Npgsql;

namespace IndividuellUppgiftDatabaser;

class Program
{
    static async Task Main(string[] args)
    {
        LoginMenu loginMenu = new LoginMenu();
        loginMenu.Display();

        await DataBaseService.CreateDatabaseTable();
    }
}
