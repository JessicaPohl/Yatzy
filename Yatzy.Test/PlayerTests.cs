using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class PlayerTests
{
    private readonly Mock<IParser> _parserMock;
    private readonly Mock<IInputOutputHandler> _ioHandlerMock;

    public PlayerTests()
    {
        _parserMock = new Mock<IParser>();
        _ioHandlerMock = new Mock<IInputOutputHandler>();
    }
    
    [Fact] 
    public void WhenGetCurrentPlayerChoice_PlayerDiceChoiceIsStoredAsCurrentChoice()
    {
        //arrange
        var player = new Player(_parserMock.Object, _ioHandlerMock.Object);
        //act
        _ioHandlerMock.Setup(x => x.GetUserInput()).Returns("5,5,5,5,-");
        player.GetCurrentPlayerChoice();
        var actualCurrentPlayerChoice = player.CurrentPlayerChoice;
        var expectedCurrentPlayerChoice = "5,5,5,5,-";
        //assert
        Assert.Equal(expectedCurrentPlayerChoice, actualCurrentPlayerChoice);
    }
    
    [Fact] 
    public void WhenGetCurrentNumberOfDiceToReRoll_CorrectNumberOfDiceToReRollIsStoredAsAvailableDice()
    {
        //arrange
        var player = new Player(_parserMock.Object, _ioHandlerMock.Object);
        //act
        _ioHandlerMock.Setup(x => x.GetUserInput()).Returns("5,5,5,5,-");
        player.GetCurrentPlayerChoice();
        _parserMock.Setup(x => x.ConvertUserInputIntoNumberOfDiceToReRoll("5,5,5,5,-")).Returns(1);
        player.GetCurrentNumberOfDiceToReRoll();
        var actualNumberOfDiceToReRoll = player.AvailableDice;
        var expectedNumberOfDiceToReRoll = 1;
        //assert
        Assert.Equal(expectedNumberOfDiceToReRoll, actualNumberOfDiceToReRoll);
    }
    
}