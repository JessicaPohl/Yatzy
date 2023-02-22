namespace Yatzy.Models;

public class Turn
{
    private readonly int _numberOfRollsLeftAtTheStart = 3;
    private int _numberOfAvailableDiceAtTheStart = 5;
    public int AvailableDice { get; set; }
    public int NumberOfRollsLeft { get; set; }

    public Turn()
    {
        NumberOfRollsLeft = _numberOfRollsLeftAtTheStart;
        AvailableDice = _numberOfAvailableDiceAtTheStart;
        var dice = new Dice();
    }
    
    public int GetNumberOfRollsLeft()
    {
        return NumberOfRollsLeft;
    }
    public void TakeTurn(Dice dice, int availableDice)
    {
        for (var i = 0; i <= NumberOfRollsLeft; i++)
        {
            dice.RollDice(availableDice);
        }
        NumberOfRollsLeft--;
    }
}
