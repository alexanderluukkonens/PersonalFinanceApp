public class LogInUserCommand : Command
{
    public LogInUserCommand(string Name, IUserService userService)
        : base(Name, userService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
