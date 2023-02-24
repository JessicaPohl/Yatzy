namespace Yatzy.Interfaces;

public interface IPlayer
{
    public string? PlayerName { get; set; }
    public string CurrentPlayerChoice { get; }
    public int AvailableDice { get; set; }
    public void GetCurrentPlayerChoice();
    public void GetCurrentNumberOfDiceToReRoll();
}