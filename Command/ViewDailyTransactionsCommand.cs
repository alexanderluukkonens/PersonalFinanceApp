public class ViewDailyTransactionsCommand : Command
{
    public ViewDailyTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("4", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        var now = DateTime.Now;
        var startDate = now.Date;
        var endDate = startDate.AddDays(1).AddSeconds(-1);

        DisplayTransactions("Daily Transactions", startDate, endDate);
    }
}
