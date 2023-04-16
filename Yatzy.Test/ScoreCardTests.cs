using Moq;
using Yatzy.Enums;
using Yatzy.Interfaces;
using Yatzy.Services;

namespace Yatzy.Test;

public class ScoreCardTests
{
    private readonly Mock<IPlayer> _playerMock;
    
    public ScoreCardTests()
    {
        _playerMock = new Mock<IPlayer>();
    }
    
    [Theory]
    [InlineData("5,6,3,3,1", ScoreCategory.Ones, 1)]
    [InlineData("1,1,1,2,2", ScoreCategory.Ones, 3)]
    [InlineData("1,1,1,1,1", ScoreCategory.Ones, 5)]  
    
    [InlineData("5,6,3,3,2", ScoreCategory.Twos, 2)]
    [InlineData("2,2,2,1,1", ScoreCategory.Twos, 6)]
    [InlineData("2,2,2,2,2", ScoreCategory.Twos, 10)]  
    
    [InlineData("5,6,2,2,3", ScoreCategory.Threes, 3)]
    [InlineData("3,3,3,1,1", ScoreCategory.Threes, 9)]
    [InlineData("3,3,3,3,3", ScoreCategory.Threes, 15)] 
    
    [InlineData("5,6,2,2,4", ScoreCategory.Fours, 4)]
    [InlineData("4,4,4,1,1", ScoreCategory.Fours, 12)]
    [InlineData("4,4,4,4,4", ScoreCategory.Fours, 20)] 
    
    [InlineData("4,6,2,2,5", ScoreCategory.Fives, 5)]
    [InlineData("5,5,5,1,1", ScoreCategory.Fives, 15)]
    [InlineData("5,5,5,5,5", ScoreCategory.Fives, 25)] 
    
    [InlineData("4,5,2,2,6", ScoreCategory.Sixes, 6)]
    [InlineData("6,6,6,1,1", ScoreCategory.Sixes, 18)]
    [InlineData("6,6,6,6,6", ScoreCategory.Sixes, 30)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryOneToSix_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("6,6,6,3,1", ScoreCategory.ThreeOfAKind, 18)] 
    [InlineData("6,5,6,5,5", ScoreCategory.ThreeOfAKind, 15)] 
    [InlineData("2,6,2,3,2", ScoreCategory.ThreeOfAKind, 6)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryThreeOfAKind_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("6,6,6,6,1", ScoreCategory.FourOfAKind, 24)] 
    [InlineData("6,5,5,5,5", ScoreCategory.FourOfAKind, 20)] 
    [InlineData("2,6,2,2,2", ScoreCategory.FourOfAKind, 8)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryFourOfAKind_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("6,6,6,3,3", ScoreCategory.FullHouse, 25)] 
    [InlineData("6,6,5,3,3", ScoreCategory.FullHouse, 0)] 
    [InlineData("1,1,2,2,2", ScoreCategory.FullHouse, 25)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryFullHouse_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("1,2,3,4,1", ScoreCategory.SmallStraight, 30)] 
    [InlineData("6,6,5,3,3", ScoreCategory.SmallStraight, 0)] 
    [InlineData("2,3,4,5,4", ScoreCategory.SmallStraight, 30)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategorySmallStraight_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("1,2,3,4,5", ScoreCategory.LargeStraight, 40)] 
    [InlineData("1,2,3,4,3", ScoreCategory.LargeStraight, 0)] 
    [InlineData("2,3,4,5,6", ScoreCategory.LargeStraight, 40)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryLargeStraight_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("1,1,1,1,1", ScoreCategory.Yatzy, 50)] 
    [InlineData("2,3,4,5,6", ScoreCategory.Yatzy, 0)] 
    [InlineData("1,1,1,1,3", ScoreCategory.Yatzy, 0)] 
    [InlineData("5,5,5,5,5", ScoreCategory.Yatzy, 50)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryYatzy_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Theory]
    [InlineData("1,1,1,1,1", ScoreCategory.Chance, 5)] 
    [InlineData("2,3,4,5,6", ScoreCategory.Chance, 20)] 
    [InlineData("1,2,3,4,5", ScoreCategory.Chance, 15)] 
    [InlineData("5,5,4,4,4", ScoreCategory.Chance, 22)] 
    public void WhenGivenSelectedCurrentPlayerDiceChoiceAndCategoryChance_CalculateScoreCorrectly(
        string currentSelectedDice, ScoreCategory category, int expectedScore)
    {
        //arrange
        _playerMock.SetupGet(x => x.ChosenCategory).Returns(category);
        _playerMock.SetupGet(x => x.CurrentPlayerChoice).Returns(currentSelectedDice);
        var scoreCard = new ScoreCard(_playerMock.Object);
        //act
        scoreCard.CalculateScore();
        var actualScore = scoreCard.TotalScore;
        //assert
        Assert.Equal(expectedScore, actualScore);
    }
}