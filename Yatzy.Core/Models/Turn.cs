using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn
{
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    private readonly int _numberOfAvailableDiceAtTheStart = 5;
    private readonly IPlayerChoice _playerChoice;
    private readonly IParser _parser;
    private readonly IIOHandler _ioHandler;
    public int AvailableDice { get; set; }
    public int NumberOfRollsLeft { get; set; }

    public Turn(IPlayerChoice playerChoice, IParser parser, IIOHandler ioHandler)
    {
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
        AvailableDice = _numberOfAvailableDiceAtTheStart;
        _playerChoice = playerChoice;
        _parser = parser;
        _ioHandler = ioHandler;
    }
    
    public string? CurrentPlayerSelectionOutput { get; set; }
    public int GetNumberOfRollsLeft()
    {
        return NumberOfRollsLeft;
    }
    public void TakeTurn(IDice dice)
    {
        //initial roll of all 5 dice
        dice.RollDice(5);
        NumberOfRollsLeft--;

        //re-roll and select dice to keep loop
        while (NumberOfRollsLeft > 0)
        {
            //player to select dice to keep
            var currentPlayerInput = _playerChoice.GetCurrentPlayerChoice();
            //save current player input as an array of int to calculate score at the end of the roll/turn
            string []currentPlayerSelection = _parser.ConvertUserInputIntoCurrentPlayerSelection(currentPlayerInput);
            CurrentPlayerSelectionOutput = currentPlayerSelection.ToString();
            //calculate dice to re-roll based on last input
            var availableDice = _parser.ConvertUserInputIntoNumberOfDiceToReRoll(currentPlayerInput);
            //reroll available dice
            if (NumberOfRollsLeft > 0)
            {
                dice.RollDice(availableDice);
                NumberOfRollsLeft--;
            }
        } //all 3 rolls done
        
        //scoring
        // output currentSelection
        _ioHandler.Print($"Your current selection is {CurrentPlayerSelectionOutput}");
        _ioHandler.Print("Which category do you want to score this turn in?");

    }
}
