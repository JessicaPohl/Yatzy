using Yatzy.Enums;

namespace Yatzy.Interfaces;

public interface IScoreCard
{
    public void AddScore(ScoreCategory category, int score);
   
    public int TotalScore { get; }
    public int GetCategoryScore(ScoreCategory category);
    void CalculateScore();
}