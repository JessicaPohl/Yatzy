using Yatzy.Interfaces;

namespace Yatzy.Controller;

public class Game
{
    private readonly ITurn _turn;
    private readonly IDice _dice;
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;
    private readonly IInputOutputHandler _inputOutputHandler;
    private readonly IScoreCard _scoreCard1;
    private readonly IScoreCard _scoreCard2;
    private int _totalNumberOfTurns = 13;

    public Game(ITurn turn, IDice dice, IPlayer player1, IPlayer player2, IInputOutputHandler inputOutputHandler,
        IScoreCard scoreCard1, IScoreCard scoreCard2)
    {
        _turn = turn;
        _dice = dice;
        _player1 = player1;
        _player2 = player2;
        _inputOutputHandler = inputOutputHandler;
        _scoreCard1 = scoreCard1;
        _scoreCard2 = scoreCard2;
    }

    public void PlayGame()
    {
        SetupGame(_player1, _player2);
        while(_totalNumberOfTurns != 0)
        {
            _inputOutputHandler.PrintTurnAnnouncement(_player1);
            _turn.TakeTurn(_dice, _player1, _scoreCard1);
            _inputOutputHandler.PrintTurnAnnouncement(_player2);
            _turn.TakeTurn(_dice, _player2, _scoreCard2);
            _totalNumberOfTurns--;
        }
        PrintFinalScores();
        DeclareWinner();
    }

    private void SetupGame(IPlayer player1, IPlayer player2)
    {
        _inputOutputHandler.Print(Constants.Messages.Welcome);
        _inputOutputHandler.Print(Constants.Messages.Player1NamePrompt);
        player1.PlayerName = _inputOutputHandler.GetUserInput();
        _inputOutputHandler.PrintCustomisedWelcome(player1);
        _inputOutputHandler.Print(Constants.Messages.Player2NamePrompt);
        player2.PlayerName = _inputOutputHandler.GetUserInput();
        _inputOutputHandler.PrintCustomisedWelcome(player2);
        _inputOutputHandler.Print(Constants.Messages.GameBegins);
    }

    private void PrintFinalScores()
    {
        _inputOutputHandler.Print(Constants.Messages.GameHasFinished);
        _inputOutputHandler.PrintTotalScore(_player1, _scoreCard1);
        _inputOutputHandler.PrintTotalScore(_player2, _scoreCard2);
    }

    private void DeclareWinner()
    {
        if (_scoreCard1.TotalScore > _scoreCard2.TotalScore)
        {
            _inputOutputHandler.PrintWinnerAnnouncement(_player1);
        }
        else if (_scoreCard1.TotalScore < _scoreCard2.TotalScore)
        {
            _inputOutputHandler.PrintWinnerAnnouncement(_player2);
        }
        else
            _inputOutputHandler.Print(Constants.Messages.DrawMessage);
    }
}
