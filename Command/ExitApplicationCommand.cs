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
        Console.Clear();
        Utilities.WaitForKey("Exit the application!");
        Program.isRunning = false;
    }
}
