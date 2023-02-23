using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class TurnTests
{
    private readonly Mock<IPlayerChoice> _playerChoiceMock;
    private readonly Mock<IParser> _parserMock;
    private readonly Mock<IDice> _diceMock;
    private readonly Mock<IIOHandler> _ioHandlerMock;

    public TurnTests()
    {
        _playerChoiceMock = new Mock<IPlayerChoice>();
        _parserMock = new Mock<IParser>();
        _diceMock = new Mock<IDice>();
        _ioHandlerMock = new Mock<IIOHandler>();
    }
    
    [Fact]
    public void WhenTurnIsTaken_PlayerCanRollDiceThreeTimes()
    {
        //arrange
        var turn = new Turn(_playerChoiceMock.Object, _parserMock.Object, _ioHandlerMock.Object, _diceMock.Object);
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
        var turn = new Turn(_playerChoiceMock.Object, _parserMock.Object, _ioHandlerMock.Object, _diceMock.Object);
        //act
        turn.TakeTurn(_diceMock.Object);
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 0;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
}