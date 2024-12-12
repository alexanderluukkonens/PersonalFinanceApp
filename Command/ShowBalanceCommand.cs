public class ShowBalanceCommand : Command
{
    public ShowBalanceCommand(string Name, IUserService userService)
        : base(Name, userService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
