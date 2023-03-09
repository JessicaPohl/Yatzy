using Yatzy.Enums;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn : ITurn
{
    private readonly IIOHandler _ioHandler;
    private readonly IDice _dice;
    private readonly IValidator _validator;
    
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    public int[] CurrentDiceRoll { get; set; }
    public int NumberOfRollsLeft { get; set; }
    public Turn(IIOHandler ioHandler, IDice dice, IValidator validator)
    {
        _ioHandler = ioHandler;
        _validator = validator;
        _dice = dice;
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
        CurrentDiceRoll = new int[5];
    }
    public void TakeTurn(IDice dice, IPlayer player, IScoreCard scoreCard)
    {
        player.AvailableDice = 5;
        while (NumberOfRollsLeft > 0)
        {
            //roll dice
            CurrentDiceRoll = dice.RollDice(player.AvailableDice);
            _ioHandler.Print($"{player.PlayerName} rolled... {_dice.GetCurrentRolledDiceFormatted(CurrentDiceRoll)}");
            
            //prompt player to make selection
            _ioHandler.Print($"Please select which dice from this roll you would like to keep (e.g. 1,3,-,-,-): ");

            //player to select dice to keep
            player.GetCurrentPlayerChoice();
            player.AddSelectedDiceToAllKeptDice(player);
            while (_validator.IsValidChoice() == false)
            {
                _ioHandler.Print("Your input was invalid, please try again: ");
                player.GetCurrentPlayerChoice();
            }
            _ioHandler.Print($"You have selected: {player.CurrentPlayerChoice}");
            player.GetCurrentNumberOfDiceToReRoll();
            NumberOfRollsLeft--;
            if (player.AvailableDice == 0)
            {
                NumberOfRollsLeft = 0;
                break;
            }
            //calculate dice to re-roll based on last input & inform player
            _ioHandler.Print($"You decided to re-roll {player.AvailableDice} dice:");
            
        }
        
        _ioHandler.Print($"How do you want to score your dice at the end of this turn: {player.CurrentPlayerChoice}? Enter the number of the category you want to score this turn as:  ");

        //print available score categories
        foreach (ScoreCategory category in Enum.GetValues((typeof(ScoreCategory))))
        {
            if (scoreCard.GetCategoryScore(category) == -1)
            {
                _ioHandler.Print($"{(int)category}: {category.ToString()}");
            }
        }
        
        //get valid score category pick from player, check choice is valid and category does not yet have a score
        while (true)
        {
            //get input and check if valid category, check valid int and compare category int to valid score category
            if (int.TryParse(_ioHandler.GetUserInput(), out var categoryInt) && Enum.IsDefined(typeof(ScoreCategory), categoryInt))
            {
                //if a valid score category is selected, set player's chosen category
                player.ChosenCategory = (ScoreCategory)categoryInt;
                
                //check if category has already been scored, if has not been scored yet, break
                if (scoreCard.GetCategoryScore(player.ChosenCategory) == -1)
                {
                    break;
                }
                {
                    _ioHandler.Print("You have already scored that category. Pick another category to score.");
                }
            }
            else
            {
                _ioHandler.Print("Invalid input. Choose a category by entering the corresponding number");
            }
        }
        
        //calculate score and print turn score
        scoreCard.CalculateScore();
        _ioHandler.Print($"You have chosen {player.ChosenCategory}, your score is: {scoreCard.GetCategoryScore(player.ChosenCategory)}");
        
        //reset number of rolls left for player 2 turn
        NumberOfRollsLeft = 3;
    }
}