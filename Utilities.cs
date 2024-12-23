using System.Net;

public class Utilities
{
    public static void WaitForKey(string text = "")
    {
        Console.WriteLine("\n" + text);
        Thread.Sleep(700);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
    public static void MenuHeading(string text = "")
    {
        Console.Clear();
        Console.WriteLine(text);
        Console.WriteLine("----------------------------------------\n");
    }


}
