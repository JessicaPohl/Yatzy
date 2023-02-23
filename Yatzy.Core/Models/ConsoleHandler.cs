using Yatzy.Interfaces;

namespace Yatzy.Models;

public class ConsoleHandler : IIOHandler
{
    public string? GetUserInput()
    {
        return Console.ReadLine();
    }

    public void Print(string message)
    {
        Console.WriteLine(message);
    }
}