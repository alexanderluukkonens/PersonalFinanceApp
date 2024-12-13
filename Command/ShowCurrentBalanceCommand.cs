public class ShowCurrentBalanceCommand : Command
{
    public ShowCurrentBalanceCommand(IUserService userService, IMenuService menuService)
        : base("3", userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("ShowCurrent transaction!");
        Console.ReadKey();
    }
}
