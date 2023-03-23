using Yatzy.Enums;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Turn : ITurn
{
    private readonly IInputOutputHandler _inputOutputHandler;
    private readonly IValidator _validator;
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    public int[] CurrentDiceRoll { get; set; }
    public int NumberOfRollsLeft { get; set; }

    public Turn(IInputOutputHandler inputOutputHandler, IValidator validator)
    {
        _inputOutputHandler = inputOutputHandler;
        _validator = validator;
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
            ValidateDiceChoice(player);
            player.AddSelectedDiceToAllKeptDice(player);
            _inputOutputHandler.PrintCurrentDiceSelection(player);
            player.GetCurrentNumberOfDiceToReRoll();
            NumberOfRollsLeft--;
            if (player.AvailableDice == 0)
            {
                NumberOfRollsLeft = 0;
                break;
            }

            _inputOutputHandler.PrintHowManyDicePickedForReRoll(player);
        }

        _inputOutputHandler.Print(Constants.Messages.ScoreCategoryInstruction);
        PrintAvailableScoreCategories(scoreCard);
        _inputOutputHandler.Print(Constants.Messages.ScoreCategoryPrompt);
        GetValidCategoryChoice(player, scoreCard);

        scoreCard.CalculateScore();
        _inputOutputHandler.PrintCategoryScore(player, scoreCard);
        NumberOfRollsLeft = 3;
    }

    private void ValidateDiceChoice(IPlayer player)
    {
        while (_validator.IsValidDiceChoice() == false)
        {
            _inputOutputHandler.Print(Constants.Messages.InvalidInput);
            player.GetCurrentPlayerChoice();
        }
    }

    private void PrintAvailableScoreCategories(IScoreCard scoreCard)
    {
        foreach (ScoreCategory category in Enum.GetValues((typeof(ScoreCategory))))
        {
            if (scoreCard.GetCategoryScore(category) == -1)
            {
                _inputOutputHandler.Print($"{(int)category}: {category.ToString()}");
            }
        }
    }

    private void GetValidCategoryChoice(IPlayer player, IScoreCard scoreCard)
    {
        int.TryParse(_inputOutputHandler.GetUserInput(), out var categoryChoice);
        
        while (!Enum.IsDefined(typeof(ScoreCategory), categoryChoice))
        {
            _inputOutputHandler.Print(Constants.Messages.InvalidCategory);

            if (scoreCard.GetCategoryScore(player.ChosenCategory) == -1)
            {
                break;
            }
            {
                _inputOutputHandler.Print(Constants.Messages.CategoryAlreadyScored);
            }
        }
        player.ChosenCategory = (ScoreCategory)categoryChoice;
    }
}
