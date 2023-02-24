using Yatzy.Interfaces;

namespace Yatzy.Controller;

public class Game
{
    private readonly ITurn _turn;
    private readonly IDice _dice;
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;
    private IPlayer _player;

    public Game(ITurn turn, IDice dice, IPlayer player1, IPlayer player2)
    {
        _turn = turn;
        _dice = dice;
        _player1 = player1;
        _player2 = player2;
    }
    
    public void PlayGame()
    {
        _turn.TakeTurn(_dice,_player);
    }
}
