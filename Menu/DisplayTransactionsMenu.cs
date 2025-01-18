public class DisplayTransactionsMenu
{

    private ITransactionService _transactionService;
    public DisplayTransactionsMenu(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public void DisplayTransactions(string title, DateTime startDate, DateTime endDate)
    {
        try
        {
            if (startDate > endDate)
            {
                Console.WriteLine("Invalid date range: Start date cannot be later than end date");
                return;
            }

            Console.WriteLine($"\n{title}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Period: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}\n");

            var transactions = _transactionService.GetTransactionsByTimespan(startDate, endDate);

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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            Utilities.WaitForKey();
        }
    }
}