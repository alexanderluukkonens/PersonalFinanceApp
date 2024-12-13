public class AddTransactionCommand : Command
{
    public AddTransactionCommand(IUserService userService, IMenuService menuService)
        : base("1", userService, menuService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Addtransaction!");
        Console.ReadKey();
    }
}
