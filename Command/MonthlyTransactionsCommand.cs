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
        DisplayTransactionsMenu displayTransactions = new DisplayTransactionsMenu(transactionService);
        try
        {
            Console.Clear();
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            displayTransactions.DisplayTransactions("Monthly Transactions", startDate, endDate);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Error: Invalid date range for monthly transactions.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving monthly transactions.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
