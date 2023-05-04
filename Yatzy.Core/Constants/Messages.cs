namespace Yatzy.Constants;

public static class Messages
{
    public const string Welcome = " 🎲 🎲 🎲 Welcome to Yatzy!  🎲 🎲 🎲";
    public const string Player1NamePrompt = "Player 1, please enter your name: ";
    public const string Player2NamePrompt = "Player 2, please enter your name: ";
    public const string WelcomePlayer = "Welcome {0}! 👋\n";
    public const string GameBegins = "The game begins!\n";
    public const string DiceSelectionPrompt = "Please select which dice from this roll you would like to keep (e.g. 1,3,-,-,-): ";
    public const string CurrentDiceSelection = "You have selected: {0} \n";
    public const string DiceForReRoll = "You decided to re-roll {0} dice. \n";

    public const string ScoreCategoryInstruction = @"
How do you want to score your dice at the end of this turn? 🎲
Please choose a category below for this turn's score.
(If you don't have suitable dice to score in a specific category, you can cross it off your list by selecting it.)";
    
    public const string ScoreCategoryPrompt = "Please enter the number of the category you have chosen: ";
    public const string InvalidInput = "Your input was invalid, please try again: ";
    public const string CategoryAlreadyScored = "You have already scored that category. Pick another category to score.";
    public const string InvalidCategory = "Invalid input. Choose a category by entering the corresponding number.";
    public const string DiceRoll = "{0} rolled... {1} \n";
    public const string PlayerCategoryScore = "You have chosen {0}, your score is: {1} \n";
    public const string GameHasFinished = "The game is finished! 🥳\n";
    public const string PlayerTotalScore = "{0}'s total score is: {1} \n";
    public const string NextTurn = "{0}'s turn! 🔄";
    public const string Winner = "{0} has won! 🎉";
    public const string DrawMessage = "Unbelievably you had the exact same score so you both won! 🎊";
}
   