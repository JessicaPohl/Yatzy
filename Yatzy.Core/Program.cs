// See https://aka.ms/new-console-template for more information

using Yatzy;
using Yatzy.Controller;
using Yatzy.Models;


var parser = new Parser();
var consoleHandler = new ConsoleHandler();
var player1 = new Player(parser, consoleHandler);
var player2 = new Player(parser, consoleHandler);
var dice = new Dice();
var scoreCard1 = new ScoreCard(player1);
var scoreCard2 = new ScoreCard(player2);
var validator = new Validator(player1, dice);
var turn = new Turn(consoleHandler, dice, validator);
var game = new Game(turn, dice, player1, player2, consoleHandler, scoreCard1, scoreCard2);
game.PlayGame();