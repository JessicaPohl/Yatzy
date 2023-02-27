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

    public bool IsValidChoice(IPlayer player, IDice dice)
    {
        string regex = @"^(-|\d),(-|\d),(-|\d),(-|\d),(-|\d)$";
        var currentPlayerChoice = _player.CurrentPlayerChoice;
        if (!Regex.IsMatch(currentPlayerChoice, regex)) return false;
        
        int[] currentSelectedDice = Regex.Matches(_player.CurrentPlayerChoice, "(-?[0-9]+)").OfType<Match>().Select(m => int.Parse(m.Value)).ToArray();
        int[] currentRolledDice = Regex.Matches(_dice.GetCurrentRolledDiceFormatted(_dice.CurrentRolledDice), "(-?[0-9]+)").OfType<Match>().Select(m => int.Parse(m.Value)).ToArray();
        
        Dictionary<int, int> countsRolledDice = new Dictionary<int, int>();
        foreach (int value in currentRolledDice)
        {
            if (countsRolledDice.ContainsKey(value))
            {
                countsRolledDice[value]++;
            }
            else
            {
                countsRolledDice[value] = 1;
            }
        }

        Dictionary<int, int> countsSelectedDice = new Dictionary<int, int>();
        foreach (int value in currentSelectedDice)
        {
            if (countsSelectedDice.ContainsKey(value))
            {
                countsSelectedDice[value]++;
            }
            else
            {
                countsSelectedDice[value] = 1;
            }
        }
        
        bool IsSelectedDiceEqualRolledDice = true;
        
        foreach (KeyValuePair<int, int> selectedDice in countsSelectedDice)
        {
            if (!countsRolledDice.ContainsKey(selectedDice.Key) || countsRolledDice[selectedDice.Key] != selectedDice.Value)
            {
                IsSelectedDiceEqualRolledDice = false;
                break;
            }
        }
        return IsSelectedDiceEqualRolledDice;
    }
}