namespace Yatzy.Interfaces;

public interface IValidator
{
    public bool IsValidChoice(IPlayer player, IDice dice);
}