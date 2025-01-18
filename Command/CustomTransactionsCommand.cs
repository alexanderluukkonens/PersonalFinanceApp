using System.Runtime.InteropServices;

public class CustomTransactionsCommand : Command
{
    public CustomTransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("5", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        DisplayTransactionsMenu displayTransactions = new DisplayTransactionsMenu(transactionService);
        Console.Clear();
        Console.WriteLine("Enter start date (yyyy-MM-dd):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            Console.WriteLine("Invalid date format");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Enter end date (yyyy-MM-dd):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
        {
            Console.WriteLine("Invalid date format");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        displayTransactions.DisplayTransactions("Custom Date Range Transactions", startDate, endDate);
    }
}
