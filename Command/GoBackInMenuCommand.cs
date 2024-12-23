public class GobackInMenuCommand : Command
{
    public GobackInMenuCommand(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base("6", userService, menuService, transactionService) { }

    public override void Execute(string inputCommand)
    {
        menuService.SetMenu(new MainMenu(userService, menuService, transactionService));
    }
}
