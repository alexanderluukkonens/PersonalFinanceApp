using Npgsql;

public class PostgresTransactionService : ITransactionService 
{
    private NpgsqlConnection connection;

    private IUserService userService;

    public PostgresTransactionService(NpgsqlConnection connection, IUserService userService)
    {
        this.connection = connection;
        this.userService = userService;
    }

    public Transaction AddTransaction()
    {
        User user = userService.GetLoggedInUser()!;
    }

    public Transaction DeleteTransaction()
    {
        User user = userService.GetLoggedInUser()!;
    }

    public Transaction ShowCurrentBalance()
    {
        User user = userService.GetLoggedInUser()!;
    }

    public Transaction ShowTransactions()
    {
        User user = userService.GetLoggedInUser()!;
    }
}
  