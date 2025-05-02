using SnakeGame.Application;
using SnakeGame.Application.Interfaces;
using SnakeGame.Core.Entities;
using SnakeGame.GraphicsUI.Framework;
using SFML.Window;

namespace SnakeGame.GraphicsUI.Adapters
{
    public class SFMLGameAdapter : IGameInput, IGameOutput, IDisposable
    {
        private readonly Screen _screen;
        private readonly int _width;
        private readonly int _height;
        private Direction? _currentDirection;
        private bool _pauseToggled;
        private bool _exitRequested;
        private bool _hasInput;

        // Game visuals configuration
        private readonly Color _backgroundColor = new Color(10, 10, 40);
        private readonly Color _snakeHeadColor = new Color(50, 255, 50);
        private readonly Color _snakeBodyColor = new Color(0, 200, 0);
        private readonly Color _foodColor = new Color(255, 50, 50);

        public SFMLGameAdapter(Screen screen)
        {
            _screen = screen;
            _width = screen.Width;
            _height = screen.Height;
        }

        public SFMLGameAdapter(int width, int height)
        {
            _width = width;
            _height = height;
            _screen = new Screen(width, height, 20); // Add border
        }

        // IGameInput Implementation
        public bool HasInput()
        {
            ProcessInput();
            return _hasInput;
        }

        public Direction? GetDirection()
        {
            var direction = _currentDirection;
            _currentDirection = null;
            _hasInput = false;
            return direction;
        }

        public bool ToggledPause()
        {
            var wasToggled = _pauseToggled;
            _pauseToggled = false;
            return wasToggled;
        }

        public bool RequestedExit()
        {
            return _exitRequested;
        }

        private void ProcessInput()
        {
            var key = _screen.PollKeyboard();
            if (key.HasValue)
            {
                _hasInput = true;

                switch (key.Value)
                {
                    case Keyboard.Key.Up:
                        _currentDirection = Direction.Up;
                        break;
                    case Keyboard.Key.Down:
                        _currentDirection = Direction.Down;
                        break;
                    case Keyboard.Key.Left:
                        _currentDirection = Direction.Left;
                        break;
                    case Keyboard.Key.Right:
                        _currentDirection = Direction.Right;
                        break;
                    case Keyboard.Key.Space:
                        _pauseToggled = true;
                        break;
                    case Keyboard.Key.Escape:
                        _exitRequested = true;
                        break;
                }
            }
        }

        // IGameOutput Implementation
        public void Display(GameState state)
        {
            _screen.Clear(_backgroundColor);

            // Draw snake
            DrawSnake(state.Snake);

            // Draw food
            DrawFood(state.Food);

            // Update status line with score
            if (state.IsPaused)
            {
                _screen.SetStatusLine($"Score: {state.Score}, PAUSED, direction: {state.Snake.NextDirection}");
            } else {
                _screen.SetStatusLine($"Score: {state.Score}");
            }

            // Refresh display
            _screen.Update();

            // Small delay to prevent screen tearing
            // Thread.Sleep(16); // ~60fps
        }

        public void DisplayGameOver(GameState state)
        {
            // Display game over message
            _screen.SetStatusLine($"GAME OVER - Final Score: {state.Score}");

            // Refresh display
            _screen.Update();

            // Small delay to let the user see the game over message before exiting
            Thread.Sleep(1000);
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetHeight()
        {
            return _height;
        }

        private void DrawSnake(Snake snake)
        {
            _screen.SetPixel(snake.Head.X, snake.Head.Y, _snakeHeadColor);
            foreach (var segment in snake.Tail)
            {
                _screen.SetPixel(segment.X, segment.Y, _snakeBodyColor);
            }
        }

        private void DrawFood(Food food)
        {
            if (food == null) return;
            _screen.SetPixel(food.Position.X, food.Position.Y, _foodColor);
        }

        public void Dispose()
        {
            _screen.Dispose();
        }
    }
}