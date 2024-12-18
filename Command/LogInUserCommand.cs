public class LogInUserCommand : Command
{
    public LogInUserCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("1", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Log In!");
        Console.Write("Enter username: ");
        string username = Console.ReadLine()!;
        Console.Write("\nEnter password: ");
        string password = Console.ReadLine()!;
        userService.Login(username, password);
        Utilities.WaitForKey("Logged in successfully!");
        menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
    }
}
