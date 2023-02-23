namespace Yatzy.Interfaces;

public interface IDice
{
    public int[] RollDice(int availableDice);
    public string GetCurrentRolledDiceFormatted(int[] currentRoll);
}