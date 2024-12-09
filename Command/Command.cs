public abstract class Command
{
    public ConsoleKey Name { get; init; }

    protected IUserService? userService;
    private int v;

    public Command(ConsoleKey Name, IUserService userService)
    {
        this.Name = Name;
        this.userService = userService;
    }

    protected Command(int v, IUserService userService)
    {
        this.v = v;
        this.userService = userService;
    }

    public abstract void Execute(string[] args);
}
