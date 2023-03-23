namespace Yatzy.Interfaces;

public interface IInputOutputHandler
{
    public string? GetUserInput();
    public void Print(string message);

    public void PrintCustomisedWelcome(IPlayer player);

    public void PrintTotalScore(IPlayer player, IScoreCard scoreCard);

    public void PrintTurnAnnouncement(IPlayer player);

    public void PrintWinnerAnnouncement(IPlayer player);

    public void PrintCurrentDiceRoll(IPlayer player, IDice dice, int[] currentDiceRoll);

    public void PrintCurrentDiceSelection(IPlayer player);

    public void PrintHowManyDicePickedForReRoll(IPlayer player);

    public void PrintCategoryScore(IPlayer player, IScoreCard scoreCard);
}