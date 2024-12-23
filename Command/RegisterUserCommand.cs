public class RegisterUserCommand : Command
{
    public RegisterUserCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Utilities.MenuHeading("Create account");
        Console.Write("Enter username: ");
        string username = Console.ReadLine()!;
        Console.Write("Enter password: ");
        string password = Console.ReadLine()!;
        userService.RegisterUser(username, password);
        Utilities.WaitForKey("Creating account successfully!");
    }
}
