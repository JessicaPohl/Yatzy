namespace Yatzy.Interfaces;

public interface IParser
{
    public int ConvertUserInputIntoNumberOfDiceToReRoll(string? userInput);
}