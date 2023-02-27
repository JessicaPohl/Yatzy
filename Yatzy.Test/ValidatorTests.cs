using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class ValidatorTests
{
    private readonly Mock<IPlayer> _playerMock;
    private readonly Mock<IDice> _diceMock;

    public ValidatorTests()
    {
        _playerMock = new Mock<IPlayer>();
        _diceMock = new Mock<IDice>();
    }

    [Theory]
    [InlineData("5,5,5,5,5", true)]
    
    [InlineData("(5,5,5,5,5)", false)]
    [InlineData("(5,5,5,5,5,1)", false)]
    [InlineData("(5,4,3,2)", false)]
    [InlineData("(-,-,-,)", false)]
    [InlineData("()", false)]
    [InlineData("", false)]
    public void WhenPlayerChoosesMoreThanFiveDice_ReturnsFalse(string currentPlayerChoice, bool expectedValidatorResult)
    {
        //arrange
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentPlayerChoice);
        _diceMock.Setup(x => x.GetCurrentRolledDiceFormatted(It.IsAny<int[]>())).Returns("5,5,5,5,5");
        //act
        var validator = new Validator(_playerMock.Object, _diceMock.Object);
        var actualValidatorResult = validator.IsValidChoice(_playerMock.Object, _diceMock.Object);
        //assert
        Assert.Equal(expectedValidatorResult, actualValidatorResult);
    }
    
    [Theory]
    [InlineData("5,5,5,5,5", "5,5,5,5,5", true)]
    [InlineData("4,4,4,4,4", "4,4,4,4,4", true)]
    [InlineData("5,5,4,4,3", "5,5,4,4,3", true)]
    [InlineData("5,5,4,4,3", "4,5,4,3,5", true)]
    [InlineData("1,2,3,-,-", "1,2,3,-,-", true)]
    [InlineData("1,2,3,-,-", "-,-,1,2,3", true)]
    
    [InlineData("5,5,5,5,5", "4,4,4,4,4", false)]
    [InlineData("4,4,4,4,4", "3,3,3,3,3", false)]
    [InlineData("5,5,4,4,3", "2,2,1,1,1", false)]
    [InlineData("1,2,-,-,-", "3,4,-,-,-", false)]
    [InlineData("1,2,3,-,-", "-,-,4,4,5", false)]
    public void WhenPlayerChoosesDiceTheyHaveNotCurrentlyRolled_ReturnsFalse(string currentRolledDice, string currentPlayerChoice, bool expectedValidatorResult)
    {
        //arrange
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentPlayerChoice);
        _diceMock.Setup(x => x.GetCurrentRolledDiceFormatted(It.IsAny<int[]>())).Returns(currentRolledDice);
        //act
        var validator = new Validator(_playerMock.Object, _diceMock.Object);
        var actualValidatorResult = validator.IsValidChoice(_playerMock.Object, _diceMock.Object);
        //assert
        Assert.Equal(expectedValidatorResult, actualValidatorResult);
    }
    
}