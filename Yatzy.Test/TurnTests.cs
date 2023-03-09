using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class TurnTests
{
    private readonly Mock<IPlayer> _playerMock;
    private readonly Mock<IParser> _parserMock;
    private readonly Mock<IDice> _diceMock;
    private readonly Mock<IInputOutputHandler> _ioHandlerMock;
    private readonly Mock<IValidator> _validatorMock;
    private readonly Mock<IScoreCard> _scoreCardMock;

    public TurnTests()
    {
        _playerMock = new Mock<IPlayer>();
        _parserMock = new Mock<IParser>();
        _diceMock = new Mock<IDice>();
        _ioHandlerMock = new Mock<IInputOutputHandler>();
        _validatorMock = new Mock<IValidator>();
        _scoreCardMock = new Mock<IScoreCard>();
    }
    //
    // [Fact]
    // public void WhenTurnIsTaken_PlayerCanRollDiceThreeTimes()
    // {
    //     //arrange
    //     var turn = new Turn(_ioHandlerMock.Object, _diceMock.Object,_validatorMock.Object);
    //     //act
    //     var actualNumberOfRollsLeft = turn.NumberOfRollsLeft;
    //     var expectedNumberOfRollsLeft = 3;
    //     //assert
    //     Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    // }
    
    // [Fact]
    // public void WhenTurnIsTaken_NumberOfRollsLeftIs0AtTheEndOfTheTurn()
    // {
    //     //arrange
    //     _playerMock.SetupGet(x => x.AvailableDice).Returns(5);
    //     _playerMock.SetupGet(x => x.PlayerName).Returns(It.IsAny<string>);
    //     _playerMock.SetupGet(x=> x.CurrentPlayerChoice).Returns("3,3,3,3,3");
    //     _validatorMock.Setup(x => x.IsValidChoice()).Returns(true);
    //     _diceMock.Setup(x=> x.RollDice(5)).Returns( new[] {3,3,3,3,3});
    //     _diceMock.Setup(x=> x.GetCurrentRolledDiceFormatted(new[] {3,3,3,3,3})).Returns( "3,3,3,3,3");
    //     _parserMock.Setup(x => x.ConvertUserInputIntoNumberOfDiceToReRoll("3,3,3,3,3")).Returns(5);
    //     
    //     var turn = new Turn(_ioHandlerMock.Object, _diceMock.Object, _validatorMock.Object);
    //     //act
    //     turn.TakeTurn(_diceMock.Object, _playerMock.Object,  _scoreCardMock.Object);
    //     var actualNumberOfRollsLeft = turn.NumberOfRollsLeft;
    //     var expectedNumberOfRollsLeft = 0;
    //     //assert
    //     Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    // }
    
}