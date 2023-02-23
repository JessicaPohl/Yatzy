using Yatzy.Interfaces;

namespace Yatzy.Models;

public class PlayerChoiceValidator : IPlayerChoiceValidator
{
    private readonly IPlayerChoice _playerChoice;
    private readonly IDice _dice;
    public PlayerChoiceValidator(IPlayerChoice playerChoice, IDice dice)
    {
        _playerChoice = playerChoice;
        _dice = dice;
    }
    public bool CheckSelection(string currentPlayerSelection, int[] currentDiceRoll)
    {
        foreach (var die in currentPlayerSelection)
        {
            if (!currentDiceRoll.ToString()!.Contains(die))
            {
                return false;
            }
            
            if (currentPlayerSelection.Contains(die))
            {
                currentPlayerSelection.Remove(die);
            }
        }

        return true;
    }
}