public class TransactionMenu : Menu
{
    public TransactionMenu() { }

    public override void Display()
    {
        Console.Write(
            """
            Transactions
            ---------------------------------------------
            [1] Show yearly transactions
            [2] Show monthly transactions
            [3] Show weekly transactions
            [4] Show daily transactions

            [5] Back to menu
                    
            Choose an option: 
            """
        );
    }
}
