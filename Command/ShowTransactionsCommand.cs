public class ShowTransactionsCommand : Command
{
    public ShowTransactionsCommand(IUserService userService, IMenuService menuService)
        : base("4", userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Show Statistic transaction!");
        Console.ReadKey();
    }
}
