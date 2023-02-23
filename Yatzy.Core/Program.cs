// See https://aka.ms/new-console-template for more information

using Yatzy;
using Yatzy.Interfaces;
using Yatzy.Models;

Console.WriteLine("Welcome to Yatzy - the game begins!");

var parser = new Parser();
var consoleHandler = new ConsoleHandler();
var playerChoice = new PlayerChoice(parser, consoleHandler);
var dice = new Dice();
var playerChoiceValidator = new PlayerChoiceValidator(playerChoice, dice);
var turn = new Turn(playerChoice, playerChoiceValidator, parser, consoleHandler, dice);
var game = new Game(turn, dice);
game.PlayGame();