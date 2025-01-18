public class GobackInMenuCommand : Command
{
    public GobackInMenuCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("6", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while returning to main menu.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
        }
    }
}
