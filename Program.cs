using Npgsql;

namespace IndividuellUppgiftDatabaser;

class Program
{
    
    static void Main(string[] args)
    {
        MainMenu mainMenu = new MainMenu();
        mainMenu.Display();
    }
}
