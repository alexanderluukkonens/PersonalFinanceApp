public class AddTransactionCommand : Command
{
    public AddTransactionCommand(string Name, IUserService userService, IMenuService menuService)
        : base(Name, userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
