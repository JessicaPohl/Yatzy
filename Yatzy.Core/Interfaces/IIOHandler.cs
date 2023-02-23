namespace Yatzy.Interfaces;

public interface IIOHandler
{
    public string? GetUserInput();
    public void Print(string message);
}