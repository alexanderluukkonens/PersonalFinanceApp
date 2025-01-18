using System.Text;

public class RegisterUserCommand : Command
{
    public RegisterUserCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        try
        {
            Console.Clear();
            Utilities.MenuHeading("Create account");

            Console.Write("Enter username: ");
            string username = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(username))
            {
                Utilities.WaitForKey("Username cannot be empty!");
                return;
            }

            Console.Write("Enter password: ");
            string password = userService.HandlePasswordInput();

            if (string.IsNullOrWhiteSpace(password))
            {
                Utilities.WaitForKey("Password cannot be empty!");
                return;
            }

            Console.Write("Confirm password: ");
            string confirmPassword = userService.HandlePasswordInput();

            if (password != confirmPassword)
            {
                Utilities.WaitForKey("Passwords do not match!");
                return;
            }

            try
            {
                userService.RegisterUser(username, password);
                Utilities.WaitForKey("Account created successfully!");
            }
            catch (Exception ex)
            {
                Utilities.WaitForKey($"Registration failed: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred during registration.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
        }
    }


}