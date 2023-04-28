using System.Text.RegularExpressions;
using Yatzy.Interfaces;

namespace Yatzy.Services;

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
        // representing a roll and re-roll selection as a Set<Dice> & taking the difference of the two sets
        // implementing an IComparator<IndexedDice> which identifies a dice by its index

        string regex = @"^(-|\d),(-|\d),(-|\d),(-|\d),(-|\d)$";
        
        var currentPlayerChoice = _player.CurrentPlayerChoice;
        var currentRolledDice = _dice.CurrentRolledDice;
        var currentRolledDiceFormatted = _dice.GetCurrentRolledDiceFormatted(currentRolledDice);
        
        if (!Regex.IsMatch(currentPlayerChoice, regex)) return false;

        var currentSelectedDiceIndexed = _player.CurrentPlayerChoice?.Split(',')
            .Where(c => char.IsDigit(Convert.ToChar(c)))
            .Select(int.Parse)
            .Select(index => new IndexedDice(index));

        var currentRolledDiceIndexed = currentRolledDiceFormatted
            .Split(',')
            .Where(c => char.IsDigit(Convert.ToChar(c)))
            .Select(int.Parse)
            .Select(index => new IndexedDice(index));

        var previousKeptDiceIndexed = _player.PreviousKeptDice.Select(d => new IndexedDice(d)).ToHashSet();
        var allKeptDiceIndexed = currentRolledDiceIndexed.Concat(previousKeptDiceIndexed).ToHashSet();

        var diceComparer = new DiceIndexEqualityComparer();
        var selectedDiceSet = currentSelectedDiceIndexed.ToHashSet(diceComparer);
        var keptDiceSet = allKeptDiceIndexed.ToHashSet((diceComparer));
        var differenceDiceSet = keptDiceSet.Except(selectedDiceSet).ToList();

        return differenceDiceSet.Count == 0;
    }

    private class IndexedDice
    {
        public int Index { get; }
 
        public IndexedDice(int index)
        {
            Index = index;
        }
         
        public override bool Equals(object? obj)
        {
            return obj is IndexedDice dice && Index == dice.Index;
        }

        public override int GetHashCode()
        {
            return Index;
        }
    }
 
    private class DiceIndexEqualityComparer : IEqualityComparer<IndexedDice>
    {
        public bool Equals(IndexedDice? x, IndexedDice? y)
        {
            return x.Index == y.Index;
        }
 
        public int GetHashCode(IndexedDice obj)
        {
            return obj.Index;
        }
    }
}