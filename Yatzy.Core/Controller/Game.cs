using Yatzy.Interfaces;

namespace Yatzy.Controller;

public class Game
{
    private readonly ITurn _turn;
    private readonly IDice _dice;
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;
    private readonly IIOHandler _ioHandler;

    public Game(ITurn turn, IDice dice, IPlayer player1, IPlayer player2, IIOHandler ioHandler)
    {
        _turn = turn;
        _dice = dice;
        _player1 = player1;
        _player2 = player2;
        _ioHandler = ioHandler;
    }
    
    public void PlayGame()
    {
        SetupGame(_player1, _player2);
        _turn.TakeTurn(_dice,_player1);
        _ioHandler.Print($"{_player2.PlayerName}'s turn!");
        _turn.TakeTurn(_dice,_player2);
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
}
