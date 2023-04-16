using Yatzy.Interfaces;

namespace Yatzy.Services;

public class Dice : IDice
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

    public string GetCurrentRolledDiceFormatted(int[] currentRoll)
    {
        List<string> currentRolledDiceList = new List<string>();
        foreach (int die in currentRoll)
        {
            currentRolledDiceList.Add(die.ToString());
        }

        string currentRolledDiceOutput = string.Join(",", currentRolledDiceList);

        return currentRolledDiceOutput;
    }
}