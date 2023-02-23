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
    
    [Theory]
    [MemberData(nameof(Data), MemberType = typeof(ParserTests))]
    public void WhenCovertUserInputIntoCurrentPlayerSelection_ReturnCorrectScorablePlayerSelection(string userInput, string[] expectedOutputArray)
    {
        //arrange
        var parser = new Parser();
        //act
        var actualOutputArray = parser.ConvertUserInputIntoCurrentPlayerSelection(userInput);
        //assert
        Assert.Equal(expectedOutputArray, actualOutputArray);
    }
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] {
                "-,-,5,5,-",
                new[] {"-","-","5","5","-"}
            },
            new object[] {
                "-,2,4,-,1",
                new[] {"-","2","4","-","1"}
            },
        };
}