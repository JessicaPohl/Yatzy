using Yatzy.Services;

namespace Yatzy.Test;

public class DiceTests
{
    [Fact]
    public void WhenDieIsRolled_ValueBetweenOneAndSixIsReturned()
    {
        //arrange
        var dice = new Dice();
        //act
        var actualDiceValue = dice.RollDie();
        //assert
        Assert.InRange(actualDiceValue, 1, 6);
    }
    
    [Theory]
    [InlineData(5, 5)]
    [InlineData(4, 4)]
    [InlineData(3, 3)]
    [InlineData(2, 2)]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    public void WhenDiceAreRolled_TheNumberOfDiceRolledEqualsTheNumberOfAvailableDice(int numberOfAvailableDice, int expectedNumberOfDiceRolled)
    {
        //arrange
        var dice = new Dice();
        //act
        dice.RollDice(numberOfAvailableDice);
        var actualNumberOfDiceRolled = dice.CurrentRolledDice.Length;
        //assert
        Assert.Equal(actualNumberOfDiceRolled, expectedNumberOfDiceRolled);
    }
    
    [Fact]
    public void WhenCurrentRolledDiceGetFormatted_CorrectFormatIsReturned()
    {
        //arrange
        var dice = new Dice();
        //act
        var actualDiceFormatted = dice.GetCurrentRolledDiceFormatted(new[] { 5, 5, 5, 5, 5 });
        var expectedDiceFormatted = "5,5,5,5,5";
        //assert
        Assert.Equal(expectedDiceFormatted, actualDiceFormatted);
    }
}