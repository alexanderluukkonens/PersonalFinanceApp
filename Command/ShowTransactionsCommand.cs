public class ShowTransactionsCommand : Command
{
    public ShowTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("4", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Show Statistic transaction!");
        Console.ReadKey();
    }
}
