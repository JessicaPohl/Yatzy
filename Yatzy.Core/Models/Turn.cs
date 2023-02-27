using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn : ITurn
{
    private readonly IIOHandler _ioHandler;
    private readonly IDice _dice;
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    private readonly IValidator _validator;
    private int[] CurrentDiceRoll { get; set; }

    public int NumberOfRollsLeft { get; set; }


    public Turn(IIOHandler ioHandler, IDice dice, IValidator validator)
    {
        _ioHandler = ioHandler;
        _dice = dice;
        _validator = validator;
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
    }
    public void TakeTurn(IDice dice, IPlayer player)
    {
        while (NumberOfRollsLeft > 0)
        {
            //roll dice
            CurrentDiceRoll = dice.RollDice(player.AvailableDice);
            _ioHandler.Print($"{player.PlayerName} rolled... {_dice.GetCurrentRolledDiceFormatted(CurrentDiceRoll)}");

            //prompt player to make selection
            _ioHandler.Print($"Please select which dice from this roll you would like to keep (e.g. (-,1,-,-,3): ");

            //player to select dice to keep
            player.GetCurrentPlayerChoice();
            while (_validator.IsValidChoice(player, dice) == false)
            {
                _ioHandler.Print("Your input was invalid, please try again: ");
                player.GetCurrentPlayerChoice();
            }
            _ioHandler.Print($"You have selected: {player.CurrentPlayerChoice}");

            //calculate dice to re-roll based on last input & inform player
            player.GetCurrentNumberOfDiceToReRoll();
            NumberOfRollsLeft--;
            if (player.AvailableDice == 0)
            {
                NumberOfRollsLeft = 0;
                return;
            }
            _ioHandler.Print($"You decided to reroll {player.AvailableDice} dice:");
        } //all 3 rolls done
        
        // _ioHandler.Print($"Your dice after this roll: {GetEndOfTurnRolledDice(player)}");
        // //scoring
        // // prompt scoring
        // _ioHandler.Print("Which category do you want to score this turn in?");

    }
    
}