public abstract class Command
{
    public string KeyName { get; init; }

    protected IUserService? userService;
    protected IMenuService menuService;

    public Command(string keyName, IUserService userService, IMenuService menuService)
    {
        this.KeyName = keyName;
        this.userService = userService;
        this.menuService = menuService;
    }

    public abstract void Execute(string inputCommand);
}
