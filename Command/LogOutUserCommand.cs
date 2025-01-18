public class LogOutUserCommand : Command
{
    public LogOutUserCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("5", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            userService.Logout();
            Console.Clear();
            menuService.SetMenu(new LoginMenu(userService, menuService, transactionService));
            Console.WriteLine("Successfully logged out!");
            Utilities.WaitForKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while logging out.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
        }
    }
}