using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn : ITurn
{
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    private readonly int _numberOfAvailableDiceAtTheStart = 5;
    private readonly IPlayerChoice _playerChoice;
    private readonly IParser _parser;
    private readonly IIOHandler _ioHandler;
    private readonly IDice _dice;
    public int AvailableDice { get; set; }
    public int NumberOfRollsLeft { get; set; }

    public int[] CurrentDiceRoll { get; set; }
    public string? CurrentPlayerInput { get; set; }


    public Turn(IPlayerChoice playerChoice, IParser parser, IIOHandler ioHandler, IDice dice)
    {
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
        AvailableDice = _numberOfAvailableDiceAtTheStart;
        _playerChoice = playerChoice;
        _parser = parser;
        _ioHandler = ioHandler;
        _dice = dice;
    }
    public int GetNumberOfRollsLeft()
    {
        return NumberOfRollsLeft;
    }

    public void TakeTurn(IDice dice)
    {
        while (NumberOfRollsLeft > 0)
        {
            NumberOfRollsLeft--;
            //roll dice
            CurrentDiceRoll = _dice.RollDice(AvailableDice);
            _ioHandler.Print($"You rolled... {_dice.GetCurrentRolledDiceFormatted(CurrentDiceRoll)}");

            //prompt player to make selection
            _ioHandler.Print($"Please select which dice from this roll you would like to keep (e.g. (-,1,-,-,3): ");

            //player to select dice to keep
            CurrentPlayerInput = _playerChoice.GetCurrentPlayerChoice();
            _ioHandler.Print($"You have selected: {CurrentPlayerInput}");

            //calculate dice to re-roll based on last input
            AvailableDice = _parser.ConvertUserInputIntoNumberOfDiceToReRoll(CurrentPlayerInput);
            if (NumberOfRollsLeft == 0) return;
            _ioHandler.Print($"You decided to reroll {AvailableDice} dice:");
        } //all 3 rolls done

        //scoring
        // prompt scoring
        _ioHandler.Print("Which category do you want to score this turn in?");

    }
}