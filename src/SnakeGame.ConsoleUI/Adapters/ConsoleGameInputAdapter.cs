using SnakeGame.Application.Interfaces;
using SnakeGame.Core.Entities;

namespace SnakeGame.ConsoleUI.Adapters;

public class ConsoleGameInputAdapter : IGameInput {
    public ConsoleKey key;

    public bool ToggledPause() {
        return key == ConsoleKey.Spacebar;
    }

    public bool RequestedExit() {
        return key == ConsoleKey.Escape;
    }

    public bool HasInput() {
        if (Console.KeyAvailable) {
            key = Console.ReadKey(true).Key;
            return true;
        }

        return false;
    }

    public Direction? GetDirection() {
        switch (key) {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                return Direction.Up;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                return Direction.Down;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                return Direction.Left;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                return Direction.Right;
            default:
                return null;
        }
    }
}