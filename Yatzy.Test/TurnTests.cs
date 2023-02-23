using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class TurnTests
{
    private readonly Mock<IPlayerChoice> _playerChoiceMock;
    private readonly Mock<IParser> _parserMock;
    private readonly Mock<IDice> _diceMock;
    
    public TurnTests()
    {
        _playerChoiceMock = new Mock<IPlayerChoice>();
        _parserMock = new Mock<IParser>();
        _diceMock = new Mock<IDice>();
    }
    
    [Fact]
    public void WhenTurnIsTaken_PlayerCanRollDiceThreeTimes()
    {
        //arrange
        var turn = new Turn(_playerChoiceMock.Object, _parserMock.Object);
        //act
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 3;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
    
    [Fact]
    public void WhenTurnIsTakenAndPlayerRollsDice_NumberOfRollsLeftDecreasesByOne()
    {
        //arrange
        _playerChoiceMock.Setup(x => x.GetCurrentPlayerChoice()).Returns("-,-,5,5,-");
        var turn = new Turn(_playerChoiceMock.Object, _parserMock.Object);
        //act
        turn.TakeTurn(_diceMock.Object);
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 2;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
}