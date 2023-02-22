namespace Yatzy.Interfaces;

public interface IParser
{
    public int ConvertUserInputIntoNumberOfDiceToReRoll(string userInput);
    public string[] ConvertUserInputIntoCurrentPlayerSelection(string userInput);
}