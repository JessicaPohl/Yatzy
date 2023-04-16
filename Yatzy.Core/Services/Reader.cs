using Yatzy.Interfaces;

namespace Yatzy.Services;

public class Reader : IReader
{
    public string? GetUserInput()
    {
        return Console.ReadLine();
    }
}