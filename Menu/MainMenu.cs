namespace IndividuellUppgiftDatabaser;

public class MainMenu : Menu
{
    public MainMenu(IUserService userService, IMenuService menuService)
    {
        AddCommand(new LogOutUserCommand(userService, menuService));
    }

    public override void Display()
    {
        Console.Write(
            """
            Welcome to your personal finance application!
            ---------------------------------------------
            [1] Add transaction
            [2] Delete transaction
            [3] Show balance
            [4] Show statistic
            [5] Log out

            [6] Exit
                    
            Choose an option: 
            """
        );
    }
}
