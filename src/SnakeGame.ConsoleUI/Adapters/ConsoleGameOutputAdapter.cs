using SnakeGame.Core.Entities;
using SnakeGame.Application;
using SnakeGame.ConsoleUI.Framework;
using SnakeGame.Application.Interfaces;

namespace SnakeGame.ConsoleUI.Adapters;

public class ConsoleGameOutputAdapter : IGameOutput
{
    private Color backgroundColor = Color.BgYellow;
    private int width = 20;
    private int height = 10;

    private readonly Color _snakeHeadColor = Color.BgBlue;
    private readonly Color _snakeBodyColor = Color.BgBlue;
    private readonly Color _foodColor = Color.BgLightRed;

    public Screen screen;

    public ConsoleGameOutputAdapter(int width = 20, int height = 10)
    {
        this.width = width;
        this.height = height;
        screen = new Screen(width, height);
    }

    public ConsoleGameOutputAdapter(Screen screen)
    {
        this.screen = screen;
        width = screen.Width;
        height = screen.Height;
    }

    public int GetWidth() => width;
    public int GetHeight() => height;

    public void SetBackgroundColor(Color color)
    {
        backgroundColor = color;
    }

    void DrawSnake(Snake snake)
    {
        if (snake.Body.Count > 0)
        {
            screen.SetPixel(snake.Head.X, snake.Head.Y, " ", _snakeHeadColor);

            foreach (var segment in snake.Tail)
            {
                screen.SetPixel(segment.X, segment.Y, " ", _snakeBodyColor);
            }
        }
    }

    void DrawFood(Food food)
    {
        screen.SetPixel(food.Position.X, food.Position.Y, " ", _foodColor);
    }

    public void Display(GameState state)
    {
        screen.Clear(backgroundColor);

        DrawSnake(state.Snake);
        DrawFood(state.Food);

        if (state.IsPaused)
        {
            screen.SetStatusLine($"Score: {state.Score}, PAUSED, direction: {state.Snake.NextDirection}", 0);
        }
        else
        {
            screen.SetStatusLine($"Score: {state.Score}", 0);
        }

        screen.Update();
    }

    public void DisplayGameOver(GameState state)
    {
        // screen.SetStatusLine($"Game over, your score was {state.Score}", 0);
        screen.SetStatusLine($"GAME OVER - Final Score: {state.Score}");

        screen.Update();

        Thread.Sleep(1000);
    }
}