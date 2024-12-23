public abstract class Command
{
    public string KeyName { get; init; }

    protected IUserService? userService;
    protected IMenuService menuService;
    protected ITransactionService transactionService;

    public Command(
        string keyName,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        this.KeyName = keyName;
        this.userService = userService;
        this.menuService = menuService;
        this.transactionService = transactionService;
    }

    public abstract void Execute(string inputCommand);

    public void DisplayTransactions(string title, DateTime startDate, DateTime endDate)
    {
        Console.WriteLine($"\n{title}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Period: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}\n");

        var transactions = transactionService.GetTransactionsByTimespan(startDate, endDate);

        if (!transactions.Any())
        {
            Console.WriteLine("No transactions found for this period.");
        }
        else
        {
            Console.WriteLine("Date\t\tAmount\t\tDescription");
            Console.WriteLine("--------------------------------------------");

            foreach (var transaction in transactions)
            {
                Console.WriteLine(
                    $"{transaction.Date:yyyy-MM-dd}\t{transaction.Amount:C2}\t{transaction.Description}"
                );
            }

            var total = transactions.Sum(t => t.Amount);
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine($"Total: {total:C2}");
        }

        Utilities.WaitForKey();
    }
}
