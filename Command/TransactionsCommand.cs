public class TransactionsCommand : Command
{
    public TransactionsCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("4", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            Console.Clear();
            var transactionMenu = new TransactionMenu(userService, menuService, transactionService);
            menuService.SetMenu(transactionMenu);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while opening the transaction menu.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
        }
    }
}