using Yatzy.Enums;

namespace Yatzy.Interfaces;

public interface IPlayer
{
    public string? PlayerName { get; set; }
    public string? CurrentPlayerChoice { get; }
    public int AvailableDice { get; set; }
    public string? GetCurrentPlayerChoice();
    public void GetCurrentNumberOfDiceToReRoll();

    public void AddSelectedDiceToAllKeptDice(IPlayer player);

    public int[] PreviousKeptDice { get; }

    public ScoreCategory ChosenCategory { get; set; }
}