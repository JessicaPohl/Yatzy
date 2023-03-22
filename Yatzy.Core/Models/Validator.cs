using System.Text.RegularExpressions;
using Yatzy.Interfaces;

namespace Yatzy.Models;

public class Validator : IValidator
{
    private readonly IPlayer _player;
    private readonly IDice _dice;

    public Validator(IPlayer player, IDice dice)
    {
        _player = player;
        _dice = dice;
    }

    public bool IsValidDiceChoice()
    {
        string regex = @"^(-|\d),(-|\d),(-|\d),(-|\d),(-|\d)$";
        var currentPlayerChoice = _player.CurrentPlayerChoice;
        if (!Regex.IsMatch(currentPlayerChoice, regex)) return false;

        List<int> currentSelectedDice = Regex.Matches(_player.CurrentPlayerChoice, "([0-9]+)")
            .Select(m => int.Parse(m.Value))
            .ToList();
        List<int> currentRolledDice = Regex
            .Matches(_dice.GetCurrentRolledDiceFormatted(_dice.CurrentRolledDice), "([0-9]+)")
            .Select(m => int.Parse(m.Value))
            .ToList();

        List<int> previousKeptDice = _player.PreviousKeptDice.ToList();
        List<int> allKeptDice = currentRolledDice.Concat(previousKeptDice).ToList();
        
        Dictionary<int, int> rolledDiceCount = new Dictionary<int, int>();

        foreach (int value in currentRolledDice)
        {
            if (rolledDiceCount.ContainsKey(value))
            {
                rolledDiceCount[value]++;
            }
            else
            {
                rolledDiceCount[value] = 1;
            }
        }
        
        Dictionary<int, int> currentSelectedDiceCount = new Dictionary<int, int>();

        foreach (int value in currentSelectedDice)
        {
            if (currentSelectedDiceCount.ContainsKey(value))
            {
                currentSelectedDiceCount[value]++;
            }
            else
            {
                currentSelectedDiceCount[value] = 1;
            }
        }
        
        Dictionary<int, int> allKeptDiceCount = new Dictionary<int, int>();
        foreach (int value in allKeptDice)
        {
            if (allKeptDiceCount.ContainsKey(value))
            {
                allKeptDiceCount[value]++;
            }
            else
            {
                allKeptDiceCount[value] = 1;
            }
        }
        
        foreach (int value in currentSelectedDice)
        {
            if (!allKeptDiceCount.ContainsKey(value))
            {
                return false;
            }

            if (currentSelectedDiceCount[value] > allKeptDiceCount[value])
            {
                return false;
            }
        }

        return true;
    }
}