// See https://aka.ms/new-console-template for more information

using Yatzy;
using Yatzy.Controller;
using Yatzy.Interfaces;
using Yatzy.Models;

Console.WriteLine("Welcome to Yatzy - the game begins!");

var parser = new Parser();
var consoleHandler = new ConsoleHandler();
var player1 = new Player(parser, consoleHandler);
var player2 = new Player(parser, consoleHandler);
var dice = new Dice();
var turn = new Turn(parser, consoleHandler, dice);
var game = new Game(turn, dice, player1, player2);
game.PlayGame();