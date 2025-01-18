public class DailyTransactionsCommand : Command
{
    public DailyTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("4", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        DisplayTransactionsMenu displayTransactions = new DisplayTransactionsMenu(transactionService);
        try
        {
            Console.Clear();
            var now = DateTime.Now;
            var startDate = now.Date;
            var endDate = startDate.AddDays(1).AddSeconds(-1);
            displayTransactions.DisplayTransactions("Daily Transactions", startDate, endDate);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving daily transactions.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.ReadKey();
        }
    }
}
