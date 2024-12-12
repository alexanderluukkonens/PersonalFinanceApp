namespace IndividuellUppgiftDatabaser;

public class LoginMenu : Menu
{
    public LoginMenu(IUserService userService, IMenuService menuService)
    {
        AddCommand(new RegisterUserCommand(userService, menuService));
        AddCommand(new LogInUserCommand(userService, menuService));
    }

    public override void Display()
    {
        Console.Write(
            """
            Welcome to your personal finance application!
            ---------------------------------------------
            [1] Log In
            [2] Create Account
            [3] Exit

            Choose an option:
            """
        );
    }
}
