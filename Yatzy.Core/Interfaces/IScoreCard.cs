using Yatzy.Enums;

namespace Yatzy.Interfaces;

public interface IScoreCard
{
    public int TotalScore { get; }
    public int GetCategoryScore(ScoreCategory category);
    public int CalculateScore(ScoreCategory selectedCategory);
}