namespace Yatzy.Models;

public class Dice
{
    private readonly Random _random = new Random();

    public int RollDie()
    {
        return _random.Next(1, 7);
    }
}