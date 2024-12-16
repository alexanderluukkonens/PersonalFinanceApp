

public class LogOutUserCommand : Command
{
    public LogOutUserCommand(IUserService userService, IMenuService menuService, ITransactionService transactionService)
        : base("5", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        userService.Logout();
        Console.Clear();
        menuService.SetMenu(new LoginMenu(userService, menuService, transactionService));
        Console.WriteLine("Logged Out");
        Console.ReadKey();
    }
}
