public class AddTransactionCommand : Command
{
    public AddTransactionCommand(string Name, IUserService userService)
        : base(Name, userService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
