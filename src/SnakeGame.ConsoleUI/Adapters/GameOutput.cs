using SnakeGame.Core.Entities;
using SnakeGame.Application;
using SnakeGame.ConsoleUI.Framework;
using SnakeGame.Application.Interfaces;

namespace SnakeGame.ConsoleUI.Adapters;

public class GameOutput : IGameOutput
{
    private Color Background { get; set; }
    public int Width = 20;
    public int Height = 10;
    public Screen screen;

    public GameOutput(int width = 20, int height = 10)
    {
        Width = width;
        Height = height;
        screen = new Screen(width, height);
    }

    public GameOutput(Screen screen)
    {
        this.screen = screen;
        Width = screen.Width;
        Height = screen.Height;
    }

    public int GetWidth() => Width;
    public int GetHeight() => Height;

    public void SetBackgroundColor(Color color) {
        Background = color;
    }

    void renderSnake(Snake snake)
    {
        if (snake.Body.Count > 0)
        {
            screen.SetPixel(snake.Head.X, snake.Head.Y, " ", Color.BgGreen);

            foreach (var segment in snake.Tail)
            {
                screen.SetPixel(segment.X, segment.Y, " ", Color.BgGreen);
            }
        }
    }

    void renderFood(Food food){
        screen.SetPixel(food.Position.X, food.Position.Y, " ", Color.BgRed);
    }

    public void Display(GameState state)
    {
        screen.Clear(Background);

        renderSnake(state.Snake);
        renderFood(state.Food);

    if (state.IsPaused) {
        screen.SetStatusLine($"Score: {state.Score}, PAUSED, direction: {state.Snake.NextDirection}", 0);
    } else {
        screen.SetStatusLine($"Score: {state.Score}", 0);
    }

        screen.Update();
    }

    public void DisplayGameOver(GameState state) {
        screen.SetStatusLine($"Game over, your score was {state.Score}", 0);
        screen.Update();
    }
}