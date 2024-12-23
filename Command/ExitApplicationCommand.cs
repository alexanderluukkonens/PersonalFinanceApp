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
        Console.WriteLine("Exit the application!");
        Utilities.WaitForKey();
        Program.isRunning = false;
    }
}
