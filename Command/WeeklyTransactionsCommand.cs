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
        DisplayTransactionsMenu displayTransactions = new DisplayTransactionsMenu(transactionService);
        try
        {
            Console.Clear();
            var now = DateTime.Now;
            var startDate = now.AddDays(-(int)now.DayOfWeek);
            var endDate = startDate.AddDays(6);
            displayTransactions.DisplayTransactions("Weekly Transactions", startDate, endDate);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving weekly transactions.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.ReadKey();
        }
    }
}
