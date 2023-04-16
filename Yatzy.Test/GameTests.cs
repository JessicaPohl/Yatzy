using Moq;
using Yatzy.Controller;
using Yatzy.Interfaces;

namespace Yatzy.Test;

public class GameTests
{
    private readonly Mock<IPlayer> _player1Mock;
    private readonly Mock<IPlayer> _player2Mock;
    private readonly Mock<IDice> _diceMock;
    private readonly Mock<IInputOutputHandler> _inputOutputHandlerMock;
    private readonly Mock<IScoreCard> _scoreCard1Mock;
    private readonly Mock<IScoreCard> _scoreCard2Mock;
    private readonly Mock<ITurn> _turnMock;

    public GameTests()
    {
        _player1Mock = new Mock<IPlayer>();
        _player2Mock = new Mock<IPlayer>();
        _diceMock = new Mock<IDice>();
        _inputOutputHandlerMock = new Mock<IInputOutputHandler>();
        _scoreCard1Mock = new Mock<IScoreCard>();
        _scoreCard2Mock = new Mock<IScoreCard>();
        _turnMock = new Mock<ITurn>();
    }
    
    [Fact]
    public void WhenGameIsPlayed_PlayersPlayATurnUntilAllCategoriesHaveBeenScored()
    {
        //arrange
        //act
        var game = new Game(_turnMock.Object, _diceMock.Object, _player1Mock.Object, _player2Mock.Object,
            _inputOutputHandlerMock.Object, _scoreCard1Mock.Object, _scoreCard2Mock.Object);
        game.PlayGame();
        //assert
        _inputOutputHandlerMock.Verify(x=> x.PrintTurnAnnouncement(_player1Mock.Object), Times.Exactly(13));
        _inputOutputHandlerMock.Verify(x=> x.PrintTurnAnnouncement(_player2Mock.Object), Times.Exactly(13));
    }
    
    [Fact]
    public void WhenGameIsFinished_FinalScoresArePrinted()
    {
        //arrange
        _player1Mock.SetupGet(x => x.PlayerName).Returns("Player1");
        _player2Mock.SetupGet(x => x.PlayerName).Returns("Player2");
        _scoreCard1Mock.SetupGet(x => x.TotalScore).Returns(195);
        _scoreCard1Mock.SetupGet(x => x.TotalScore).Returns(180);
        //act
        var game = new Game(_turnMock.Object, _diceMock.Object, _player1Mock.Object, _player2Mock.Object,
            _inputOutputHandlerMock.Object, _scoreCard1Mock.Object, _scoreCard2Mock.Object);
        game.PlayGame();
        //assert
        _inputOutputHandlerMock.Verify(x=> x.PrintTotalScore(_player1Mock.Object, _scoreCard1Mock.Object), Times.Once);
        _inputOutputHandlerMock.Verify(x=> x.PrintTotalScore(_player2Mock.Object, _scoreCard2Mock.Object), Times.Once);
    }
    
    [Fact]
    public void WhenGameIsFinished_WinnerIsAnnounced()
    {
        //arrange
        _player1Mock.SetupGet(x => x.PlayerName).Returns("Player1");
        _player2Mock.SetupGet(x => x.PlayerName).Returns("Player2");
        _scoreCard1Mock.SetupGet(x => x.TotalScore).Returns(195);
        _scoreCard1Mock.SetupGet(x => x.TotalScore).Returns(180);
        //act
        var game = new Game(_turnMock.Object, _diceMock.Object, _player1Mock.Object, _player2Mock.Object,
            _inputOutputHandlerMock.Object, _scoreCard1Mock.Object, _scoreCard2Mock.Object);
        game.PlayGame();
        //assert
        _inputOutputHandlerMock.Verify(x=> x.PrintWinnerAnnouncement(_player1Mock.Object), Times.Once);
    }
}