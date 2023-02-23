using Yatzy.Interfaces;

namespace Yatzy;

public class PlayerChoice : IPlayerChoice

{
    private string _currentPlayerChoice;
    private string[] _currentSelectedDice = new string[5];
    private readonly IParser _parser;

    public PlayerChoice(IParser parser)
    {
        _parser = parser;
    }
    
    public string GetCurrentPlayerChoice()
    {
        
        return _currentPlayerChoice;
    }

    public void GetCurrentSelectedDice()
    {
        _currentSelectedDice = _parser.ConvertUserInputIntoCurrentPlayerSelection(_currentPlayerChoice);
    }
}