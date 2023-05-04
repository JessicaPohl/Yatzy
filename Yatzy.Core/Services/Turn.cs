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
            var selectedDice = GetValidSelectedDice(player);
            _writer.PrintCurrentDiceSelection(selectedDice);

            player.GetCurrentNumberOfDiceToReRoll();
            NumberOfRollsLeft--;
            
            if (player.AvailableDice == 0)
            {
                NumberOfRollsLeft = 0;
                break;
            }
            _writer.PrintHowManyDicePickedForReRoll(player);
        }
        
        var selectedCategory = GetValidSelectedCategory(player, scoreCard);
        scoreCard.CalculateScore(selectedCategory);
        _writer.PrintCategoryScore(player, scoreCard);
        
        NumberOfRollsLeft = 3;
    }

    private int[] GetValidSelectedDice(IPlayer player)
    {
        var selectedDice = new int[5];
        
        _writer.Print(Constants.Messages.DiceSelectionPrompt);
        ValidateDiceChoice(player);
        player.AddSelectedDiceToAllKeptDice(player);

        return selectedDice;
    }
    
    private ScoreCategory GetValidSelectedCategory(IPlayer player, IScoreCard scoreCard)
    {
        _writer.Print(Constants.Messages.ScoreCategoryInstruction);
        PrintAvailableScoreCategories(scoreCard);
        _writer.Print(Constants.Messages.ScoreCategoryPrompt);
        return GetValidCategoryChoice(player, scoreCard);
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

    private ScoreCategory GetValidCategoryChoice(IPlayer player, IScoreCard scoreCard)
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
        return player.ChosenCategory = (ScoreCategory)categoryChoice;
    }
}
