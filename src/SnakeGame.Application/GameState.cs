using SnakeGame.Core.Entities;

namespace SnakeGame.Application;

public class GameState 
{
    public int Score { get; set; }
    public Snake Snake { get; set; }
    public Food Food { get; set; }
    public bool IsPaused { get; set; }
}