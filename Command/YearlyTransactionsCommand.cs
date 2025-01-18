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
        DisplayTransactionsMenu displayTransactions = new DisplayTransactionsMenu(transactionService);
        try
        {
            Console.Clear();
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, 1, 1);
            var endDate = startDate.AddYears(1).AddDays(-1);
            displayTransactions.DisplayTransactions("Yearly Transactions", startDate, endDate);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving yearly transactions.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.ReadKey();
        }
    }
}
