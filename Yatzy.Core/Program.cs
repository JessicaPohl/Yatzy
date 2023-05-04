using Yatzy.Controller;
using Yatzy.Services;

var gameBuilder = new GameBuilder();
var game = gameBuilder
    .WithPlayer1(new Player(new Parser(), new Reader(), new Writer()))
    .WithPlayer2(new Player(new Parser(), new Reader(), new Writer()))
    .WithDice(new Dice())
    .WithScoreCard1(new ScoreCard(new Player(new Parser(), new Reader(), new Writer())))
    .WithScoreCard2(new ScoreCard(new Player(new Parser(), new Reader(), new Writer())))
    .Build();

game.PlayGame();

public class GameBuilder
{
    private readonly Parser _parser;
    private readonly Reader _reader;
    private readonly Writer _writer;
    private Dice _dice;
    private ScoreCard _scoreCard1;
    private ScoreCard _scoreCard2;

    private Player _player1;
    private Player _player2;
    private Validator _validator;
    private Turn _turn;

    public GameBuilder()
    {
        _parser = new Parser();
        _reader = new Reader();
        _writer = new Writer();
        _player1 = new Player(_parser, _reader, _writer);
        _player2 = new Player(_parser, _reader, _writer);
        _dice = new Dice();
        _scoreCard1 = new ScoreCard(_player1);
        _scoreCard2 = new ScoreCard(_player2);
        _validator = new Validator(_player1, _dice);
        _turn = new Turn(_reader, _writer, _validator);
    }

    public GameBuilder WithPlayer1(Player player)
    {
        _player1 = player;
        _validator = new Validator(_player1, _dice);
        _turn = new Turn(_reader, _writer, _validator);
        return this;
    }

    public GameBuilder WithPlayer2(Player player)
    {
        _player2 = player;
        return this;
    }

    public GameBuilder WithDice(Dice dice)
    {
        _dice = dice;
        _validator = new Validator(_player1, _dice);
        _turn = new Turn(_reader, _writer, _validator);
        return this;
    }

    public GameBuilder WithScoreCard1(ScoreCard scoreCard)
    {
        _scoreCard1 = scoreCard;
        return this;
    }

    public GameBuilder WithScoreCard2(ScoreCard scoreCard)
    {
        _scoreCard2 = scoreCard;
        return this;
    }

    public Game Build()
    {
        return new Game(_turn, _dice, _player1, _player2, _reader, _writer, _scoreCard1, _scoreCard2);
    }
}