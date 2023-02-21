using Yatzy.Models;

namespace Yatzy.Test;

public class DiceTests
{
    [Fact]
    public void WhenDiceIsRolled_ValueBetweenOneAndSixIsReturned()
    {
        //arrange
        var dice = new Dice();
        //act
        var actualDiceValue = dice.RollDie();
        //assert
        Assert.InRange(actualDiceValue, 1, 6);
    }
}