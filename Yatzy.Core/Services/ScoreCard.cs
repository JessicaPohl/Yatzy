using System.Text.RegularExpressions;
using Yatzy.Enums;
using Yatzy.Interfaces;

namespace Yatzy.Services;

public class ScoreCard : IScoreCard
{
    public int TotalScore { get; private set; }
    private readonly Dictionary<ScoreCategory, int> _scores = new ();
    private readonly IPlayer _player;

    public ScoreCard(IPlayer player)
    {
        foreach (ScoreCategory category in Enum.GetValues(typeof(ScoreCategory)))
        {
            _scores[category] = -1;
        }

        _player = player;

    }
    
    public int CalculateScore()
    {
        int score = 0;
        var currentSelectedDice = Regex.Matches(_player.CurrentPlayerChoice, "([0-9]+)")
                    .Select(m => int.Parse(m.Value))
                    .ToList();
        
        switch (_player.ChosenCategory)
            {
                case ScoreCategory.Ones:
                    score = GetScoreForNumber(1, currentSelectedDice);
                    break;
                case ScoreCategory.Twos:
                    score = GetScoreForNumber(2, currentSelectedDice);
                    break;
                case ScoreCategory.Threes:
                    score = GetScoreForNumber(3, currentSelectedDice);
                    break;
                case ScoreCategory.Fours:
                    score = GetScoreForNumber(4, currentSelectedDice);
                    break;
                case ScoreCategory.Fives:
                    score = GetScoreForNumber(5, currentSelectedDice);
                    break;
                case ScoreCategory.Sixes:
                    score = GetScoreForNumber(6, currentSelectedDice);
                    break;
                case ScoreCategory.ThreeOfAKind:
                    score = GetScoreForThreeOfAKind(currentSelectedDice);
                    break;
                case ScoreCategory.FourOfAKind:
                    score = GetScoreForFourOfAKind(currentSelectedDice);
                    break;
                case ScoreCategory.FullHouse:
                    score = GetScoreForFullHouse(currentSelectedDice);
                    break;
                case ScoreCategory.SmallStraight:
                    score = GetScoreForSmallStraight(currentSelectedDice);
                    break;
                case ScoreCategory.LargeStraight:
                    score = GetScoreForLargeStraight(currentSelectedDice);
                    break;
                case ScoreCategory.Yatzy:
                    score = GetScoreForYatzy(currentSelectedDice);
                    break;
                case ScoreCategory.Chance:
                    score = GetScoreForChance(currentSelectedDice);
                    break;
            }
        AddScore(_player.ChosenCategory, score);
        SetTotalScore();
        return score;
    }

    private void AddScore(ScoreCategory category, int score)
    {
        _scores[category] = score;
    }

    private void SetTotalScore()
    {
        int totalScore = 0;
        foreach (int score in _scores.Values)
        {
            if (score > 0)
            {
                totalScore += score;
            }
        }
        TotalScore = totalScore;
    }

    public int GetCategoryScore(ScoreCategory category)
    {
        return _scores[category];
    }
    private int GetScoreForNumber(int target, List<int> currentSelectedDice)
        {
            int score = 0;
            foreach (var number in currentSelectedDice)
            {
                if (number == target)
                {
                    score += number;
                }
            }
            return score;
        }

    private int GetScoreForThreeOfAKind(List<int> currentSelectedDice)
        {
            int score = 0;
            foreach (var number in currentSelectedDice.Distinct())
            {
                int count = currentSelectedDice.Count(n => n == number);
                if (count == 3) score += number * 3;
            }
            return score;
        }

    private int GetScoreForFourOfAKind(List<int> currentSelectedDice)
        {
            int score = 0;
            foreach (var number in currentSelectedDice.Distinct())
            {
                int count = currentSelectedDice.Count(n => n == number);
                if (count == 4) score += number * 4;
            }
            return score;
        }

    private int GetScoreForFullHouse(List<int> currentSelectedDice)
        {
            List<int> occurrencesOfEveryNumber = new List<int>();
            foreach (var number in currentSelectedDice)
            {
                int count = currentSelectedDice.Count(n => n == number);
                occurrencesOfEveryNumber.Add(count);
            }

            if (occurrencesOfEveryNumber.Contains(2) && occurrencesOfEveryNumber.Contains(3))
            {
                return 25;
            } 
            return 0;
        }

    private int GetScoreForSmallStraight(List<int> currentSelectedDice)
        {
            var currentSelectedDiceValuesInARow = currentSelectedDice.OrderBy(die => die).Distinct().ToList();
            if (currentSelectedDiceValuesInARow.Count < 4) return 0;
            return 30;
        }

    private int GetScoreForLargeStraight(List<int> currentSelectedDice)
        {
            var currentSelectedDiceValuesInARow = currentSelectedDice.OrderBy(die => die).Distinct().ToList();
            if (currentSelectedDiceValuesInARow.Count < 5) return 0;
            return 40;
        }

    private int GetScoreForYatzy(List<int> currentSelectedDice)
    {
        return currentSelectedDice.Distinct().Count() == 1 ? 50 : 0;
    }

    private int GetScoreForChance(List<int> currentSelectedDice)
        {
            int score = 0;
            foreach (int number in currentSelectedDice)
            {
                score += number;
            }
            return score;
        }
}