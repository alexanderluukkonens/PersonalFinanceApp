public class DeleteTransactionCommand : Command
{
    public DeleteTransactionCommand(IUserService userService, IMenuService menuService)
        : base("2", userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Delete transaction!");
        Console.ReadKey();
    }
}
