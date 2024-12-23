public class TransactionMenu : Menu
{
    public TransactionMenu(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        AddCommand(new YearlyTransactionsCommand(userService, menuService, transactionService));
        AddCommand(
            new MonthlyTransactionsCommand(userService, menuService, transactionService)
        );
        AddCommand(new WeeklyTransactionsCommand(userService, menuService, transactionService));
        AddCommand(new DailyTransactionsCommand(userService, menuService, transactionService));
        AddCommand(new CustomTransactionsCommand(userService, menuService, transactionService));
        AddCommand(new GobackInMenuCommand(userService, menuService, transactionService));
    }

    public override void Display()
    {
        Console.Write(
            """
            Transactions
            ---------------------------------------------
            [1] Yearly transactions
            [2] Monthly transactions
            [3] Weekly transactions
            [4] Daily transactions
            [5] Custom date range

            [6] Back to main menu
                   
            Choose an option: 
            """
        );
    }
}
