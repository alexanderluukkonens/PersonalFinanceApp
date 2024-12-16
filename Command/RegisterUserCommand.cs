public class RegisterUserCommand : Command
{
    public RegisterUserCommand(IUserService userService, IMenuService menuService, ITransactionService transactionService)
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Register User!");
        Console.Write("Enter username: ");
        string username = Console.ReadLine()!;
        Console.Write("\nEnter password: ");
        string password = Console.ReadLine()!;
        userService.RegisterUser(username, password);
    }
}
