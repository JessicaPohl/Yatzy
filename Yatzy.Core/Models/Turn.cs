namespace Yatzy.Models;

public class Turn
{
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    public int NumberOfRollsLeft { get; set; }

    public Turn(Dice dice)
    {
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
        dice = new Dice();
    }
    
    public int GetNumberOfRollsLeft()
    {
        return NumberOfRollsLeft;
    }
    public void TakeTurn(Dice dice)
    {
        dice.RollDice(NumberOfRollsLeft);
        NumberOfRollsLeft--;
    }
}
