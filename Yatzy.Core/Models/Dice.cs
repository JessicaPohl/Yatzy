namespace Yatzy.Models;

public class Dice
{
    public int AvailableDice { get; set; }
    private readonly Random _random = new Random();

    public Dice()
    {
        AvailableDice = 5;
    }
    public int RollDie()
    {
        return _random.Next(1, 7);
    }

    public int[] RollDice(int availableDice)
    {
        int[] rolledDice = new int[availableDice];
        for (var i = 0; i < rolledDice.Length; i++)
        {
            rolledDice[i] = RollDie();
        }
    
        return rolledDice;
    }
}