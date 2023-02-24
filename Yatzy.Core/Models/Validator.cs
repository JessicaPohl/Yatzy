using System.Text.RegularExpressions;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Validator : IValidator
{

    public bool ValidatePlayerDiceChoice(IPlayer player)
    {
        string regex = @"^\((5|-),(5|-),(5|-),(5|-),(5|-)\)$";
        var currentPlayerChoice = player.CurrentPlayerChoice;
        return Regex.IsMatch(currentPlayerChoice, regex);
    }
}