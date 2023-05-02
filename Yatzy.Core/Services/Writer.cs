using Yatzy.Interfaces;

namespace Yatzy.Services;

public class Writer : IWriter
{
    public void Print(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintCustomisedWelcome(IPlayer player)
    {
        Console.WriteLine(Constants.Messages.WelcomePlayer, player.PlayerName);
    }

    public void PrintTotalScore(IPlayer player, IScoreCard scoreCard)
    {
        Console.WriteLine(Constants.Messages.PlayerTotalScore, player.PlayerName, scoreCard.TotalScore);
    }

    public void PrintTurnAnnouncement(IPlayer player)
    {
        Console.WriteLine(Constants.Messages.NextTurn, player.PlayerName);
    }

    public void PrintWinnerAnnouncement(IPlayer player)
    {
        Console.WriteLine(Constants.Messages.Winner, player.PlayerName);
    }

    public void PrintCurrentDiceRoll(IPlayer player, IDice dice, int[] currentDiceRoll)
    {
        Console.WriteLine(Constants.Messages.DiceRoll, player.PlayerName,
            dice.GetCurrentRolledDiceFormatted(currentDiceRoll));
    }

    public void PrintCurrentDiceSelection(int[] selectedDice)
    {
        Console.WriteLine(Constants.Messages.CurrentDiceSelection, selectedDice);
    }

    public void PrintHowManyDicePickedForReRoll(IPlayer player)
    {
        Console.WriteLine(Constants.Messages.DiceForReRoll, player.AvailableDice);
    }

    public void PrintCategoryScore(IPlayer player, IScoreCard scoreCard)
    {
        Console.WriteLine(Constants.Messages.PlayerCategoryScore, player.ChosenCategory,
            scoreCard.GetCategoryScore(player.ChosenCategory));
    }
}