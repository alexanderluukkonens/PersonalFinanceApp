public class DeleteTransactionCommand : Command
{
    public DeleteTransactionCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Delete Transaction");
        Console.WriteLine("----------------------------------------\n");

        try
        {
            var transactions = transactionService
                .GetTransactionsByTimespan(DateTime.Now.AddYears(-1), DateTime.Now)
                .ToList();

            if (!transactions.Any())
            {
                Utilities.WaitForKey("No transactions found to delete.");
                return;
            }

            Console.WriteLine("Your recent transactions:");
            Console.WriteLine("Index\tDate\t\tAmount\t\tDescription");
            Console.WriteLine("----------------------------------------");

            // Visa transaktioner med index
            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                Console.WriteLine(
                    $"{i + 1}\t{transaction.Date:yyyy-MM-dd}\t{transaction.Amount:C2}\t{transaction.Description}"
                );
            }

            Console.WriteLine("\nEnter index number to delete (or press Enter to cancel):");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Operation cancelled.");
                Console.ReadKey();
                return;
            }

            if (!int.TryParse(input, out int index) || index < 1 || index > transactions.Count)
            {
                Utilities.WaitForKey("Invalid index number.");
                return;
            }

            // Hämta transaktionen baserat på index
            var selectedTransaction = transactions[index - 1];

            // Visa detaljer och bekräfta borttagning
            Console.WriteLine("\nSelected transaction:");
            Console.WriteLine($"Date: {selectedTransaction.Date:yyyy-MM-dd}");
            Console.WriteLine($"Amount: {selectedTransaction.Amount:C2}");
            Console.WriteLine($"Description: {selectedTransaction.Description}");

            Console.Write("\nAre you sure you want to delete this transaction? (y/n): ");
            if (Console.ReadLine()?.ToLower() != "y")
            {
                Utilities.WaitForKey("Deletion cancelled.");
                return;
            }

            transactionService.DeleteTransaction(selectedTransaction.Transaction_id);
            Utilities.WaitForKey("Transaction successfully deleted!");
        }
        catch (Exception ex)
        {
            Utilities.WaitForKey($"Error: {ex.Message}");
        }
    }
}
