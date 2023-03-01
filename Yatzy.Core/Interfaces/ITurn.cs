namespace Yatzy.Interfaces;

public interface ITurn
{
    public void TakeTurn(IDice dice, IPlayer player, IScoreCard scoreCard);
    public int NumberOfRollsLeft { get; set; }
    public int[] CurrentDiceRoll { get; set; }
}