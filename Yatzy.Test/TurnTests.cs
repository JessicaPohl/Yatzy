using Yatzy.Models;

namespace Yatzy.Test;

public class TurnTests
{
    [Fact]
    public void WhenTurnIsTaken_PlayerCanRollDiceThreeTimes()
    {
        //arrange
        var dice = new Dice();
        var turn = new Turn(dice);
        //act
        //turn.TakeTurn();
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 3;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
    
    [Fact]
    public void WhenTurnIsTakenAndPlayerRollsDice_NumberOfRollsLeftDecreasesByOne()
    {
        //arrange
        var dice = new Dice();
        var turn = new Turn(dice);
        //act
        turn.TakeTurn(dice);
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 2;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
}