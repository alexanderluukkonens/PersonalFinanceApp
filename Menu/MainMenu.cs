namespace IndividuellUppgiftDatabaser;

public class MainMenu : Menu
{
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
                [5] Exit
                
                Choose an option: 
            """
        );
    }
}
