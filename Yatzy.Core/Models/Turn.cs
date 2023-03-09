using Yatzy.Enums;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn : ITurn
{
    private readonly IInputOutputHandler _inputOutputHandler;
    private readonly IDice _dice;
    private readonly IValidator _validator;
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    public int[] CurrentDiceRoll { get; set; }
    public int NumberOfRollsLeft { get; set; }
    public Turn(IInputOutputHandler inputOutputHandler, IDice dice, IValidator validator)
    {
        _inputOutputHandler = inputOutputHandler;
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
            CurrentDiceRoll = dice.RollDice(player.AvailableDice);
            _inputOutputHandler.PrintCurrentDiceRoll(player, dice, CurrentDiceRoll);
            
            _inputOutputHandler.Print(Constants.Messages.DiceSelectionPrompt);
            player.GetCurrentPlayerChoice();
            player.AddSelectedDiceToAllKeptDice(player);
            while (_validator.IsValidChoice() == false)
            {
                _inputOutputHandler.Print(Constants.Messages.InvalidInput);
                player.GetCurrentPlayerChoice();
            }
            _inputOutputHandler.PrintCurrentDiceSelecion(player);
            player.GetCurrentNumberOfDiceToReRoll();
            NumberOfRollsLeft--;
            if (player.AvailableDice == 0)
            {
                NumberOfRollsLeft = 0;
                break;
            }
            _inputOutputHandler.PrintHowManyDicePickedForReRoll(player);
        }
        
        _inputOutputHandler.Print(Constants.Messages.ScoreCategoryPrompt);
        foreach (ScoreCategory category in Enum.GetValues((typeof(ScoreCategory))))
        {
            if (scoreCard.GetCategoryScore(category) == -1)
            {
                _inputOutputHandler.Print($"{(int)category}: {category.ToString()}");
            }
        }
 
        while (true)
        {
            if (int.TryParse(_inputOutputHandler.GetUserInput(), out var categoryInt) && Enum.IsDefined(typeof(ScoreCategory), categoryInt))
            {

                player.ChosenCategory = (ScoreCategory)categoryInt;
                if (scoreCard.GetCategoryScore(player.ChosenCategory) == -1)
                {
                    break;
                }
                {
                    _inputOutputHandler.Print(Constants.Messages.CategoryAlreadyScored);
                }
            }
            else
            {
                _inputOutputHandler.Print(Constants.Messages.InvalidCategory);
            }
        }
        
        scoreCard.CalculateScore();
        _inputOutputHandler.PrintCategoryScore(player, scoreCard);
        NumberOfRollsLeft = 3;
    }
}