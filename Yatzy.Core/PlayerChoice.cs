using Yatzy.Interfaces;

namespace Yatzy;

public class PlayerChoice : IPlayerChoice

{
    public string CurrentPlayerChoice;
    
    
    public string GetCurrentPlayerChoice()
    {
        
        return CurrentPlayerChoice;
    }
}