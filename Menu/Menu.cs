namespace IndividuellUppgiftDatabaser;

public abstract class Menu
{
    private List<Command> commands = new List<Command>();

    public void AddCommand(Command command)
    {
        commands.Add(command);
    }

    public abstract void Display();

    public void ExecuteCommand(string inputCommand)
    {
        foreach (Command command in commands)
        {
            if (inputCommand == command.KeyName)
            {
                command.Execute(inputCommand);
                Console.WriteLine(inputCommand);
            }
        }
    }
}
