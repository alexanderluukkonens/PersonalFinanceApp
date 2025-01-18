using System.Text;

class LogInUserCommand : Command
{
    public LogInUserCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("1", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            Utilities.MenuHeading("Log in");

            Console.Write("Enter username: ");
            string? username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Utilities.WaitForKey("Username cannot be empty. Press any key to try again.");
                return;
            }

            Console.Write("Enter password: ");
            string password = userService.HandlePasswordInput();

            if (string.IsNullOrWhiteSpace(password))
            {
                Utilities.WaitForKey("Password cannot be empty. Press any key to try again.");
                return;
            }

            var user = userService.Login(username, password);
            if (user == null)
            {
                Utilities.WaitForKey("Invalid username or password! Press any key to try again.");
                return;
            }

            Utilities.WaitForKey("Logged in successfully!");
            menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during login.");
            Console.WriteLine($"Error details: {ex.Message}");
            Utilities.WaitForKey("Press any key to try again.");
        }
    }


}