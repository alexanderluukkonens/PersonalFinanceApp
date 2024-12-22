public class TransactionMenu : Menu
{
    public TransactionMenu(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        AddCommand(new ViewYearlyTransactionsCommand(userService, menuService, transactionService));
        AddCommand(
            new ViewMonthlyTransactionsCommand(userService, menuService, transactionService)
        );
        AddCommand(new ViewWeeklyTransactionsCommand(userService, menuService, transactionService));
        AddCommand(new ViewDailyTransactionsCommand(userService, menuService, transactionService));
        AddCommand(new ViewCustomTransactionsCommand(userService, menuService, transactionService));
    }

    public override void Display()
    {
        Console.Write(
            """
            Transactions
            ---------------------------------------------
            [1] Show yearly transactions
            [2] Show monthly transactions
            [3] Show weekly transactions
            [4] Show daily transactions
            [5] View custom date range

            [6] Back to main menu
                   
            Choose an option:
            """
        );
    }
}
