public class ShowCurrentBalanceCommand : Command
{
    public ShowCurrentBalanceCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("3", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("ShowCurrent transaction!");
        Console.WriteLine(transactionService.ShowCurrentBalance());
        Console.ReadKey();
    }
}
