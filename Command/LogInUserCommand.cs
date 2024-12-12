using IndividuellUppgiftDatabaser;

public class LogInUserCommand : Command
{
    public LogInUserCommand(IUserService userService, IMenuService menuService)
        : base("1", userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Log In!");
        Console.Write("Enter username: ");
        string username = Console.ReadLine()!;
        Console.Write("\nEnter password: ");
        string password = Console.ReadLine()!;
        userService.Login(username, password);
        menuService.SetMenu(new MainMenu(userService, menuService));
    }
}
