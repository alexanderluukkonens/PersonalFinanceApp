public class RegisterUserCommand : Command
{
    public RegisterUserCommand(IUserService userService, IMenuService menuService)
        : base("2", userService, menuService) { }

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
