using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class ValidatorTests
{
    private readonly Mock<IPlayer> _playerMock;

    public ValidatorTests()
    {
        _playerMock = new Mock<IPlayer>();
    }

    [Theory]
    [InlineData("(5,5,5,5,5)", true)]
    [InlineData("(5,5,5,5,5,1)", false)]
    [InlineData("(5,4,3,2)", false)]
    [InlineData("(-,-,-,)", false)]
    [InlineData("()", false)]
    [InlineData("", false)]
    public void WhenPlayerChoosesMoreThanFiveDice_ReturnsFalse(string currentPlayerChoice, bool expectedValidatorResult)
    {
        //arrange
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentPlayerChoice);
        //act
        var validator = new Validator();
        var actualValidatorResult = validator.ValidatePlayerDiceChoice(_playerMock.Object);
        //assert
        Assert.Equal(expectedValidatorResult, actualValidatorResult);
    }
}