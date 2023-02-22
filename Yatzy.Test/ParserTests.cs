namespace Yatzy.Test;

public class ParserTests
{
    [Theory]
    [InlineData("-,-,5,5,-", 3)]
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