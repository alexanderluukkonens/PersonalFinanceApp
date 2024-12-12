public abstract class Command
{
    public string KeyName { get; init; }

    protected IUserService? userService;

    public Command(string keyName, IUserService userService)
    {
        KeyName = keyName;
        this.userService = userService;
    }

    public abstract void Execute(string inputCommand);
}
