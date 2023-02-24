using Yatzy.Interfaces;

namespace Yatzy;

public class Player : IPlayer

{
    private string? _currentPlayerChoice = "";
    private string[] _currentSelectedDice = new string[5];
    private readonly IParser _parser;
    private readonly IIOHandler _ioHandler;
    public string? CurrentPlayerChoice { get; }

    public Player(IParser parser, IIOHandler ioHandler)
    {
        _parser = parser;
        _ioHandler = ioHandler;
        CurrentPlayerChoice = _currentPlayerChoice;
    }
    
    public string? GetCurrentPlayerChoice() //gets input string
    {
        _currentPlayerChoice = _ioHandler.GetUserInput();
        return _currentPlayerChoice;
    }

    public void GetCurrentSelectedDice() //converts input into number of dice to reroll
    {
        _currentSelectedDice = _parser.ConvertUserInputIntoCurrentPlayerSelection(_currentPlayerChoice);
    }
}