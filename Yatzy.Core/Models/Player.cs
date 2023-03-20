using System.Text.RegularExpressions;
using Yatzy.Enums;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Player : IPlayer

{ 
    private readonly string? _playerName = "";
    private readonly int _numberOfAvailableDiceAtTheStart = 5;
    private readonly string? _currentPlayerChoice = "";
    private readonly IParser _parser;
    private readonly IInputOutputHandler _inputOutputHandler;
    public string? PlayerName { get; set; }
    public string? CurrentPlayerChoice { get; set; }
    public int AvailableDice { get; set; }
    public int[] PreviousKeptDice { get; set; }
    
    public ScoreCategory ChosenCategory { get; set; }

    public Player(IParser parser, IInputOutputHandler inputOutputHandler)
    {
        _parser = parser;
        _inputOutputHandler = inputOutputHandler;
        CurrentPlayerChoice = _currentPlayerChoice;
        PlayerName = _playerName;
        AvailableDice = _numberOfAvailableDiceAtTheStart;
        PreviousKeptDice = new int[5];
    }


    public string? GetCurrentPlayerChoice() 
    {
        CurrentPlayerChoice = _inputOutputHandler.GetUserInput();
        return CurrentPlayerChoice;
    }

    public void GetCurrentNumberOfDiceToReRoll() 
    {
        AvailableDice = _parser.ConvertUserInputIntoNumberOfDiceToReRoll(CurrentPlayerChoice);
    }
    
    public void AddSelectedDiceToAllKeptDice(IPlayer player)
    {
        PreviousKeptDice = (Regex.Matches(player.CurrentPlayerChoice, "([0-9]+)").Select(m => int.Parse(m.Value)).ToArray());
    }

}