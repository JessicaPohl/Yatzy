using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn
{
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    private int _numberOfAvailableDiceAtTheStart = 5;
    private readonly IPlayerChoice _playerChoice;
    private readonly IParser _parser;
    public int AvailableDice { get; set; }
    public int NumberOfRollsLeft { get; set; }

    public Turn(IPlayerChoice playerChoice, IParser parser)
    {
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
        AvailableDice = _numberOfAvailableDiceAtTheStart;
        var dice = new Dice();
        _playerChoice = playerChoice;
        _parser = parser;
    }
    
    public int GetNumberOfRollsLeft()
    {
        return NumberOfRollsLeft;
    }
    public void TakeTurn(IDice dice)
    {
        var availableDice = _parser.ConvertUserInputIntoNumberOfDiceToReRoll(_playerChoice.GetCurrentPlayerChoice());
        for (var i = 0; i <= NumberOfRollsLeft; i++)
        {
            dice.RollDice(availableDice);
        }
        NumberOfRollsLeft--;

        _playerChoice.GetCurrentPlayerChoice();
        int numberOfDiceToReRoll = _parser.ConvertUserInputIntoNumberOfDiceToReRoll(_playerChoice.GetCurrentPlayerChoice());
        string[] currentPlayerSelection = _parser.ConvertUserInputIntoCurrentPlayerSelection(_playerChoice.GetCurrentPlayerChoice());

    }
}
