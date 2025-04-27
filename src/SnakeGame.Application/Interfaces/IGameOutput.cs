namespace SnakeGame.Application.Interfaces;

public interface IGameOutput
{
    void Display(GameState state);
    void DisplayGameOver(GameState state);
    int GetWidth();
    int GetHeight();
}