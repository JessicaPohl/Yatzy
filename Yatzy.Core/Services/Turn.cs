using Yatzy.Enums;
using Yatzy.Interfaces;

namespace Yatzy.Services;

public class Turn : ITurn
{
    private readonly IReader _reader;
    private readonly IWriter _writer;
    private readonly IValidator _validator;
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    public int[] CurrentDiceRoll { get; set; }
    public int NumberOfRollsLeft { get; set; }

    public Turn(IReader reader, IWriter writer, IValidator validator)
    {
        _reader = reader;
        _writer = writer;
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
            _writer.PrintCurrentDiceRoll(player, dice, CurrentDiceRoll);
            _writer.Print(Constants.Messages.DiceSelectionPrompt);
            player.GetCurrentPlayerChoice();
            ValidateDiceChoice(player);
            player.AddSelectedDiceToAllKeptDice(player);
            _writer.PrintCurrentDiceSelection(player);
            player.GetCurrentNumberOfDiceToReRoll();
            NumberOfRollsLeft--;
            if (player.AvailableDice == 0)
            {
                NumberOfRollsLeft = 0;
                break;
            }

            _writer.PrintHowManyDicePickedForReRoll(player);
        }

        _writer.Print(Constants.Messages.ScoreCategoryInstruction);
        PrintAvailableScoreCategories(scoreCard);
        _writer.Print(Constants.Messages.ScoreCategoryPrompt);
        GetValidCategoryChoice(player, scoreCard);

        scoreCard.CalculateScore();
        _writer.PrintCategoryScore(player, scoreCard);
        NumberOfRollsLeft = 3;
    }

    private void ValidateDiceChoice(IPlayer player)
    {
        while (_validator.IsValidDiceChoice() == false)
        {
            _writer.Print(Constants.Messages.InvalidInput);
            player.GetCurrentPlayerChoice();
        }
    }

    private void PrintAvailableScoreCategories(IScoreCard scoreCard)
    {
        foreach (ScoreCategory category in Enum.GetValues((typeof(ScoreCategory))))
        {
            if (scoreCard.GetCategoryScore(category) == -1)
            {
                _writer.Print($"{(int)category}: {category.ToString()}");
            }
        }
    }

    private void GetValidCategoryChoice(IPlayer player, IScoreCard scoreCard)
    {
        int.TryParse(_reader.GetUserInput(), out var categoryChoice);
        
        while (!Enum.IsDefined(typeof(ScoreCategory), categoryChoice))
        {
            _writer.Print(Constants.Messages.InvalidCategory);

            if (scoreCard.GetCategoryScore(player.ChosenCategory) == -1)
            {
                break;
            }
            {
                _writer.Print(Constants.Messages.CategoryAlreadyScored);
            }
        }
        player.ChosenCategory = (ScoreCategory)categoryChoice;
    }
}
