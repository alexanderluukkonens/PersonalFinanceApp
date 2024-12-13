using IndividuellUppgiftDatabaser;

public class LogOutUserCommand : Command
{
    public LogOutUserCommand(IUserService userService, IMenuService menuService)
        : base("5", userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        userService.Logout();
        Console.Clear();
        menuService.SetMenu(new LoginMenu(userService, menuService));
        Console.WriteLine("Logged Out");
        Console.ReadKey();
    }
}
