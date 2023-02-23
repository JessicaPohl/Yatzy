using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Game
{
    private readonly ITurn _turn;
    private readonly IDice _dice;
    
    public Game(ITurn turn, IDice dice)
    {
        _turn = turn;
        _dice = dice;
    }
    
    public void PlayGame()
    {
        _turn.TakeTurn(_dice);
    }
}
