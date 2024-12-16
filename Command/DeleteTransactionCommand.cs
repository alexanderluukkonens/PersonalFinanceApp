public class DeleteTransactionCommand : Command
{
    public DeleteTransactionCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("2", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        Console.Clear();
        Console.WriteLine("Delete Transaction!");

        Console.Write("Enter transaction ID to delete: ");

        Guid transactionId;

        if (!Guid.TryParse(Console.ReadLine(), out transactionId))
        {
            Console.WriteLine("Invalid transaction ID.");
            Console.ReadKey();
            return;
        }

        try
        {
            transactionService.DeleteTransaction(transactionId);
            Console.WriteLine($"Transaction was succesfully deleted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadKey();
    }
}
