public class ShowBalanceCommand : Command
{
    public ShowBalanceCommand(string Name, IUserService userService, IMenuService menuService)
        : base(Name, userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
