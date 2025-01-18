public class AddTransactionCommand : Command
{
    public AddTransactionCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("1", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            Utilities.MenuHeading("Add transaction");

            decimal amount;
            while (true)
            {
                Console.Write("Enter amount (negative for expenses): ");
                string? amountInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(amountInput))
                {
                    Console.WriteLine("Amount cannot be empty. Please try again.");
                    continue;
                }

                if (decimal.TryParse(amountInput, out amount))
                {
                    break;
                }

                Console.WriteLine("Invalid amount format. Please enter a valid number.");
            }

            string? description;
            while (true)
            {
                Console.Write("Enter description: ");
                description = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("Description cannot be empty. Please try again.");
                    continue;
                }

                if (description.Length > 100)
                {
                    Console.WriteLine("Description is too long (max 100 characters). Please try again.");
                    continue;
                }

                break;
            }

            transactionService.AddTransaction(amount, description);
            menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
            Utilities.WaitForKey("Transaction added successfully!");
        }
        catch (Exception ex)
        {
            Utilities.WaitForKey($"An error occurred: {ex.Message}. Please try again.");
            menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
        }
    }
}