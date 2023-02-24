using System.Text.RegularExpressions;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Validator : IValidator
{

    public bool IsValidChoice(IPlayer player)
    {
        string regex = @"^(-|\d),(-|\d),(-|\d),(-|\d),(-|\d)$";
        var currentPlayerChoice = player.CurrentPlayerChoice;
        return Regex.IsMatch(currentPlayerChoice, regex);
    }
}