using Yatzy.Interfaces;

namespace Yatzy;

public class Parser : IParser
{
    private int _numberOfDiceToReRoll = 0;

    // var userInput = _ioHandler.GetUserInput();
    //string userInput = "-,-,5,5,-";

    public int ConvertUserInputIntoNumberOfDiceToReRoll(string userInput)
    {
        var splitUserInput = userInput.Split(",");
        foreach (string diceValue in splitUserInput)
        {
            if (diceValue == "-") _numberOfDiceToReRoll++;
        }
        return _numberOfDiceToReRoll;
    }
    
    public string[] ConvertUserInputIntoCurrentPlayerSelection(string userInput)
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