using Yatzy.Interfaces;

namespace Yatzy.Controller;

public class Game
{
    private readonly ITurn _turn;
    private readonly IDice _dice;
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;
    private readonly IIOHandler _ioHandler;
    private readonly IScoreCard _scoreCard1;
    private readonly IScoreCard _scoreCard2;

    public Game(ITurn turn, IDice dice, IPlayer player1, IPlayer player2, IIOHandler ioHandler, IScoreCard scoreCard1, IScoreCard scoreCard2)
    {
        _turn = turn;
        _dice = dice;
        _player1 = player1;
        _player2 = player2;
        _ioHandler = ioHandler;
        _scoreCard1 = scoreCard1;
        _scoreCard2 = scoreCard2;
    }
    
    public void PlayGame()
    {
        SetupGame(_player1, _player2);
        for (var i = 0; i <= 12; i++)
        { 
            _ioHandler.Print($"{_player1.PlayerName}'s turn!");
            _turn.TakeTurn(_dice,_player1, _scoreCard1);
            _ioHandler.Print($"{_player2.PlayerName}'s turn!");
            _turn.TakeTurn(_dice,_player2, _scoreCard2);
        }
        PrintFinalScores();
        DeclareWinner();
    }
    
    private void SetupGame(IPlayer player1, IPlayer player2)
    {
        _ioHandler.Print("Welcome to Yatzy!");
        _ioHandler.Print("Player 1, please enter your name: ");
        player1.PlayerName = _ioHandler.GetUserInput();
        _ioHandler.Print($"Welcome {player1.PlayerName}!");
        _ioHandler.Print("Player 2, please enter your name: ");
        player2.PlayerName = _ioHandler.GetUserInput();
        _ioHandler.Print($"Welcome {player2.PlayerName}!");
        _ioHandler.Print("The game begins!");
    }
    
    private void PrintFinalScores()
    {
       _ioHandler.Print($"The game is finished!");
       _ioHandler.Print($"{_player1.PlayerName}'s total score is: {_scoreCard1.TotalScore}");
       _ioHandler.Print($"{_player2.PlayerName}'s total score is: {_scoreCard2.TotalScore}");
    }
    
    private void DeclareWinner()
    {
        if (_scoreCard1.TotalScore > _scoreCard2.TotalScore)
        {
            _ioHandler.Print($"{_player1.PlayerName} has won!");
        } else if (_scoreCard1.TotalScore < _scoreCard2.TotalScore)
        {
            _ioHandler.Print($"{_player2.PlayerName} has won!");
        }
        else  _ioHandler.Print($"Unbelievably you had the exact same score so you both won!");
    }
}
