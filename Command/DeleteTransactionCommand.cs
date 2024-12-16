public class DeleteTransactionCommand : Command
{
    public DeleteTransactionCommand(IUserService userService, IMenuService menuService, ITransactionService transactionService)
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Delete transaction!");
        Console.ReadKey();
    }
}
