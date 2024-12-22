using System.Reflection;

public class MainMenu : Menu
{
    public MainMenu(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        AddCommand(new LogOutUserCommand(userService, menuService, transactionService));
        AddCommand(new AddTransactionCommand(userService, menuService, transactionService));
        AddCommand(new DeleteTransactionCommand(userService, menuService, transactionService));
        AddCommand(new ShowCurrentBalanceCommand(userService, menuService, transactionService));
        AddCommand(new ShowTransactionsCommand(userService, menuService, transactionService));
        AddCommand(
            new ExitApplicationCommand(userService, menuService, transactionService)
            {
                KeyName = "6",
            }
        );
    }

    public override void Display()
    {
        Console.Write(
            """
            Welcome to your personal finance application!
            ---------------------------------------------
            [1] Add transaction
            [2] Delete transaction
            [3] Show current balance 
            [4] Show transactions
            [5] Log out

            [6] Exit
                    
            Choose an option: 
            """
        );
    }
}
