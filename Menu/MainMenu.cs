namespace IndividuellUppgiftDatabaser;

public class MainMenu : Menu
{
    public MainMenu(IUserService userService, IMenuService menuService)
    {
        AddCommand(new LogOutUserCommand(userService, menuService));
        AddCommand(new AddTransactionCommand(userService, menuService));
        AddCommand(new DeleteTransactionCommand(userService, menuService));
        AddCommand(new ShowCurrentBalanceCommand(userService, menuService));
        AddCommand(new ShowTransactionsCommand(userService, menuService));
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
