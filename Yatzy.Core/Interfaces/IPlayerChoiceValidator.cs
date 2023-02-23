namespace Yatzy.Interfaces;

public interface IPlayerChoiceValidator
{
    public bool CheckSelection(string currentPlayerSelection, int[] currentDiceRoll);
}