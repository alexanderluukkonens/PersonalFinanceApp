public class MonthlyTransactionsCommand : Command
{
    public MonthlyTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        var now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        DisplayTransactions("Monthly Transactions", startDate, endDate);
    }
}
