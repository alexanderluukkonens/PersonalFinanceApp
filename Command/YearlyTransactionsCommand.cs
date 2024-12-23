public class YearlyTransactionsCommand : Command
{
    public YearlyTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("1", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        var now = DateTime.Now;
        var startDate = new DateTime(now.Year, 1, 1);
        var endDate = startDate.AddYears(1).AddDays(-1);

        DisplayTransactions("Yearly Transactions", startDate, endDate);
    }
}
