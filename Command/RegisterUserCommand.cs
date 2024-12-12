public class RegisterUserCommand : Command
{
    public RegisterUserCommand(IUserService userService)
        : base("2", userService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Register Menu!");
        Console.ReadKey();
    }
}
