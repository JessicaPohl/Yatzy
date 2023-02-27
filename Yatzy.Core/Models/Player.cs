using System.Text.RegularExpressions;
using Yatzy.Interfaces;

namespace Yatzy;

public class Player : IPlayer

{
    private readonly string? _playerName = "";
 
    private readonly int _numberOfAvailableDiceAtTheStart = 5;
    
    private readonly string? _currentPlayerChoice = "";
    private readonly IParser _parser;
    private readonly IIOHandler _ioHandler;
    public string? PlayerName { get; set; }
    public string CurrentPlayerChoice { get; set; }
    public int AvailableDice { get; set; }
    
    public int[] EndOfTurnSelection { get; set; }

    public Player(IParser parser, IIOHandler ioHandler)
    {
        _parser = parser;
        _ioHandler = ioHandler;
        CurrentPlayerChoice = _currentPlayerChoice;
        PlayerName = _playerName;
      
        AvailableDice = _numberOfAvailableDiceAtTheStart;
        EndOfTurnSelection = new int[5];
    }

    public void GetCurrentPlayerChoice() 
    {
        CurrentPlayerChoice = _ioHandler.GetUserInput();
    }

    public void GetCurrentNumberOfDiceToReRoll() 
    {
        AvailableDice = _parser.ConvertUserInputIntoNumberOfDiceToReRoll(CurrentPlayerChoice);
    }
    
    private void AddSelectedDiceToEndOfTurnSelection(IPlayer player)
    {
        EndOfTurnSelection = (Regex.Matches(player.CurrentPlayerChoice, "(-?[0-9]+)").OfType<Match>().Select(m => int.Parse(m.Value)).ToArray());
    }

}