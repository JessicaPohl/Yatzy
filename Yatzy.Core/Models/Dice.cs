namespace Yatzy.Models;

public class Dice
{
  
    private readonly Random _random = new Random();
    public int[] CurrentRolledDice { get; set; }
    
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

        CurrentRolledDice = rolledDice;
        return rolledDice;
    }
}