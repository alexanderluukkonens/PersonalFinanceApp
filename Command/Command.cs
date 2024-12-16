public abstract class Command
{
    public string KeyName { get; init; }

    protected IUserService? userService;
    protected IMenuService menuService;
    protected ITransactionService transactionService;

    public Command(
        string keyName,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        this.KeyName = keyName;
        this.userService = userService;
        this.menuService = menuService;
        this.transactionService = transactionService;
    }

    public abstract void Execute(string inputCommand);
}
