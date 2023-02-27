namespace Yatzy.Interfaces;

public interface ITurn
{
    public void TakeTurn(IDice dice, IPlayer player);
    // public string GetEndOfTurnRolledDice(IPlayer player);
    

}