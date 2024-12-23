public class WeeklyTransactionsCommand : Command
{
    public WeeklyTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("3", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        var now = DateTime.Now;
        var startDate = now.AddDays(-(int)now.DayOfWeek);
        var endDate = startDate.AddDays(6);

        DisplayTransactions("Weekly Transactions", startDate, endDate);
    }
}
