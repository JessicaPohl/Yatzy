using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class TurnTests
{
    private readonly Mock<IPlayer> _playerMock;
    private readonly Mock<IParser> _parserMock;
    private readonly Mock<IDice> _diceMock;
    private readonly Mock<IIOHandler> _ioHandlerMock;

    public TurnTests()
    {
        _playerMock = new Mock<IPlayer>();
        _parserMock = new Mock<IParser>();
        _diceMock = new Mock<IDice>();
        _ioHandlerMock = new Mock<IIOHandler>();
    }
    
    [Fact]
    public void WhenTurnIsTaken_PlayerCanRollDiceThreeTimes()
    {
        //arrange
        var turn = new Turn(_parserMock.Object, _ioHandlerMock.Object, _diceMock.Object);
        //act
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 3;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
    
    [Fact]
    public void WhenTurnIsTaken_NumberOfRollsLeftIs0AtTheEndOfTheTurn()
    {
        //arrange
        var turn = new Turn(_parserMock.Object, _ioHandlerMock.Object, _diceMock.Object);
        //act
        turn.TakeTurn(_diceMock.Object, _playerMock.Object);
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 0;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
}