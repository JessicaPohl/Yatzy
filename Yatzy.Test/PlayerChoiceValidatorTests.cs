using Moq;
using Yatzy.Interfaces;
using Yatzy.Models;

namespace Yatzy.Test;

public class PlayerChoiceValidatorTests
{
    private readonly Mock<IPlayerChoice> _playerChoiceMock;
    private readonly Mock<IDice> _diceMock;

    public PlayerChoiceValidatorTests()
    {
        _playerChoiceMock = new Mock<IPlayerChoice>();
        _diceMock = new Mock<IDice>();
    }

    [Theory]
    [MemberData(nameof(Data), MemberType = typeof(PlayerChoiceValidatorTests))]
    public void WhenPlayerPicksDiceTheyHaveNotRolled_PlayerGetsPromptedToReenterSelection(string playerChoice,
        int[] diceRolled, bool expectedResultOfCheckSelection)
    {
        //arrange
        var playerChoiceValidator = new PlayerChoiceValidator(_playerChoiceMock.Object, _diceMock.Object);
        _playerChoiceMock.Setup(x => x.GetCurrentPlayerChoice()).Returns(playerChoice);
        //_diceMock.Setup(x => x.RollDice(5)).Returns(diceRolled);
        //act
        var actualResultOfCheckSelection = playerChoiceValidator.CheckSelection(playerChoice, diceRolled);
        //assert
        Assert.Equal(expectedResultOfCheckSelection, actualResultOfCheckSelection);

    }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] {
                "6,6,6,6,6",
                new[] {"4","2","4","4","4"},
                false,
            },
            // new object[] {
            //     "-,2,4,-,1",
            //     new[] {"-","2","4","-","1"}
            // },
        };
}