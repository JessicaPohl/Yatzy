using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Parser : IParser
{
    public int ConvertUserInputIntoNumberOfDiceToReRoll(string? userInput)
    {
        var numberOfDiceToReRoll = 0;
        foreach (char diceValue in userInput)
        {
            if (diceValue == '-') numberOfDiceToReRoll++;
        }

        return numberOfDiceToReRoll;
    }

    public string[] ConvertUserInputIntoCurrentPlayerSelection(string? userInput)
    {
        var splitUserInput = userInput.Split(",");

        List<string> list = new List<string>();

        foreach (string diceValue in splitUserInput)
        {
            list.Add(diceValue);
        }

        return list.ToArray();
    }
}