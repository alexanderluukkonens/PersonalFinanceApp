public class RegisterUserCommand : Command
{
    public RegisterUserCommand(ConsoleKey Name, IUserService userService)
        : base(Name, userService) { }

    public override void Execute(string[] args)
    {
        Console.WriteLine("Register Menu!");
    }
}
