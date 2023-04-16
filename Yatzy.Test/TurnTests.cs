using Moq;
using Yatzy.Enums;
using Yatzy.Interfaces;
using Yatzy.Services;

namespace Yatzy.Test;

public class TurnTests
{
    private readonly Mock<IPlayer> _playerMock;
    private readonly Mock<IDice> _diceMock;
    private readonly Mock<IInputOutputHandler> _inputOutputHandlerMock;
    private readonly Mock<IScoreCard> _scoreCardMock;
    private readonly Mock<IValidator> _validatorMock;

    public TurnTests()
    {
        _playerMock = new Mock<IPlayer>();
        _diceMock = new Mock<IDice>();
        _inputOutputHandlerMock = new Mock<IInputOutputHandler>();
        _scoreCardMock = new Mock<IScoreCard>();
        _validatorMock = new Mock<IValidator>();
    }

    [Fact]
    public void TurnWithThreeDiceRolls_CallsTurnMethodsExpectedNumberOfTimes()
    {
        //arrange
        _playerMock.SetupProperty(x => x.AvailableDice, 5);
        _diceMock.SetupSequence(x => x.RollDice(5))
            .Returns(new[] {1, 3, 5, 2, 1})
            .Returns(new[] {2, 4, 1, 3, 4})
            .Returns(new[] {1, 1, 1, 1, 1});
        _inputOutputHandlerMock.SetupSequence(x => x.GetUserInput())
            .Returns("0");
        _validatorMock.Setup(x => x.IsValidDiceChoice())
            .Returns(true);
        _playerMock.SetupSequence(x => x.GetCurrentPlayerChoice())
            .Returns("-,-,-,-,-")
            .Returns("-,-,-,-,-")
            .Returns("1, 1, 1, 1, 1");
        _scoreCardMock.Setup(x => x.GetCategoryScore(ScoreCategory.Ones))
            .Returns(-1);
        _scoreCardMock.Setup(x => x.CalculateScore());

        var turn = new Turn(_inputOutputHandlerMock.Object, _validatorMock.Object);

        //act
        turn.TakeTurn(_diceMock.Object, _playerMock.Object, _scoreCardMock.Object);

        //assert
        _inputOutputHandlerMock.Verify(
            x => x.PrintCurrentDiceRoll(_playerMock.Object, _diceMock.Object, new[] {1, 3, 5, 2, 1}),
            Times.Once); 
        _inputOutputHandlerMock.Verify(
            x => x.PrintCurrentDiceRoll(_playerMock.Object, _diceMock.Object, new[] {2, 4, 1, 3, 4}),
            Times.Once);
        _inputOutputHandlerMock.Verify(
            x => x.PrintCurrentDiceRoll(_playerMock.Object, _diceMock.Object, new[] {1, 1, 1, 1, 1}),
            Times.Once);
        _playerMock.VerifySet(x => x.AvailableDice = 5, Times.Once);
        _diceMock.Verify(x => x.RollDice(5), Times.Exactly(3));
        _playerMock.Verify(x => x.GetCurrentPlayerChoice(), Times.Exactly(3));
        _inputOutputHandlerMock.Verify(x => x.PrintCurrentDiceSelection(_playerMock.Object), Times.Exactly(3));
        _playerMock.Verify(x => x.GetCurrentNumberOfDiceToReRoll(), Times.Exactly(3));
        _playerMock.Verify(x => x.AddSelectedDiceToAllKeptDice(_playerMock.Object), Times.Exactly(3));
        _inputOutputHandlerMock.Verify(x => x.PrintHowManyDicePickedForReRoll(_playerMock.Object), Times.Exactly(3));
        _inputOutputHandlerMock.Verify(x => x.Print(Constants.Messages.ScoreCategoryPrompt), Times.Once);
        _scoreCardMock.Verify(x => x.CalculateScore(), Times.Once);
        _inputOutputHandlerMock.Verify(x => x.PrintCategoryScore(_playerMock.Object, _scoreCardMock.Object),
            Times.Once);
    }
}