using IndividuellUppgiftDatabaser;

public interface IMenuService
{
    void SetMenu(Menu menu);
    Menu GetMenu();
}

public class SimpleMenuService : IMenuService
{
    private Menu menu = new EmptyMenu();

    public Menu GetMenu()
    {
        return menu;
    }

    public void SetMenu(Menu menu)
    {
        this.menu = menu;
    }
}

class EmptyMenu : Menu
{
    public override void Display() { }
}
