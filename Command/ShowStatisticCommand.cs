public class ShowStatisticCommand : Command
{
    public ShowStatisticCommand(string Name, IUserService userService, IMenuService menuService)
        : base(Name, userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        throw new NotImplementedException();
    }
}
