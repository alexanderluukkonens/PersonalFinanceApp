public class AddTransactionCommand : Command
{
    public AddTransactionCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("1", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Addtransaction!");
        Console.Write("Enter amount: ");
        decimal amount = decimal.Parse(Console.ReadLine()!);
        Console.Write("Enter description: ");
        string description = Console.ReadLine()!;
        transactionService.AddTransaction(amount, description);
        menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
        Console.ReadKey();
    }
}
