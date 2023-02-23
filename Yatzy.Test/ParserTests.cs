namespace Yatzy.Test;

public class ParserTests
{
    [Theory]
    [InlineData("-,-,5,5,-", 3)]
    [InlineData("-,5,5,5,-", 2)]
    [InlineData("-,3,2,1,4", 1)]
    public void WhenCovertUserInputIntoNUmberOfDiceToReRoll_ReturnCorrectNumberOfDiceToReRoll(string userInput, int expectedNumberOfDiceToReRoll)
    {
        //arrange
        var parser = new Parser();
        //act
        var actualNumberOfDiceToReRoll = parser.ConvertUserInputIntoNumberOfDiceToReRoll(userInput);
        //assert
        Assert.Equal(expectedNumberOfDiceToReRoll, actualNumberOfDiceToReRoll);
    }
}