public class CurrentBalanceCommand : Command
{
    public CurrentBalanceCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("3", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            Utilities.MenuHeading("Current balance");

            var balance = transactionService.ShowCurrentBalance();
            if (balance == null)
            {
                Console.WriteLine("Error: Unable to retrieve balance. Please try again later.");
                return;
            }

            Console.WriteLine(balance);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred while retrieving your balance.");
            Console.WriteLine($"Error details: {ex.Message}");
        }
        finally
        {
            Utilities.WaitForKey();
        }
    }
}
