public class DeleteTransactionCommand : Command
{
    public DeleteTransactionCommand(string Name, IUserService userService)
        : base(Name, userService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
