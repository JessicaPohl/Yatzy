using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class TurnTests
{
    [Fact]
    public void WhenTurnIsTaken_PlayerCanRollDiceThreeTimes()
    {
        //arrange
        var playerChoice = new PlayerChoice();
        var parser = new Parser();
        var turn = new Turn(playerChoice, parser);
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
        var playerChoiceMock = new Mock<IPlayerChoice>();
        var parser = new Parser();
        playerChoiceMock.Setup(x => x.GetCurrentPlayerChoice()).Returns("-,-,5,5,-");
        var turn = new Turn(playerChoiceMock.Object, parser);
        //act
        turn.TakeTurn(dice);
        var actualNumberOfRollsLeft = turn.GetNumberOfRollsLeft();
        var expectedNumberOfRollsLeft = 2;
        //assert
        Assert.Equal(expectedNumberOfRollsLeft, actualNumberOfRollsLeft);
    }
}