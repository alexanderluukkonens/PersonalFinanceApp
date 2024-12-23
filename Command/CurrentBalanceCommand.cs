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
        Utilities.MenuHeading("Current balance");
        Console.WriteLine(transactionService.ShowCurrentBalance());
        Utilities.WaitForKey();
    }
}
