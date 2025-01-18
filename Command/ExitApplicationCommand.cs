public class ExitApplicationCommand : Command
{
    public ExitApplicationCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("3", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Exiting the application...");
            Utilities.WaitForKey();
            Program.isRunning = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while trying to exit the application.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine("Press any key to try exiting again.");
            Console.ReadKey();
        }
    }
}
