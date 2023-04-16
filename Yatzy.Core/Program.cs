using Yatzy.Controller;
using Yatzy.Services;

var parser = new Parser();
var reader = new Reader();
var writer = new Writer();
var player1 = new Player(parser, reader, writer);
var player2 = new Player(parser, reader, writer);
var dice = new Dice();
var scoreCard1 = new ScoreCard(player1);
var scoreCard2 = new ScoreCard(player2);
var validator = new Validator(player1, dice);
var turn = new Turn(reader, writer, validator);
var game = new Game(turn, dice, player1, player2, reader, writer, scoreCard1, scoreCard2);

game.PlayGame();