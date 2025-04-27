using SnakeGame.Application.Interfaces;
using SnakeGame.Core.Entities;

namespace SnakeGame.Application;

public class Game
{
    private Snake snake { get; set; }
    private Food food { get; set; }
    private GameBoard gameBoard { get; set; }
    private int score = 0;
    private bool isGameOver = false;
    private bool isPaused = false;
    private int updateInterval = 100;

    private readonly IGameOutput _gameOutput;
    private readonly IGameInput _gameInput;
    public Game(IGameOutput gameOutput, IGameInput gameInput)
    {
        _gameOutput = gameOutput;
        _gameInput = gameInput;

        gameBoard = new GameBoard(gameOutput.GetWidth(), gameOutput.GetHeight());
        snake = new Snake(new Position(gameBoard.Width / 2, gameBoard.Height / 2), Direction.Right);

        SpawnFood();
    }
    public void Run()
    {
        while (!isGameOver)
        {
            if (_gameInput.HasInput())
            {
                if (_gameInput.ToggledPause())
                {
                    isPaused = !isPaused;
                }

                if (_gameInput.RequestedExit())
                {
                    isGameOver = true;
                }

                var direction = _gameInput.GetDirection();
                if (direction.HasValue)
                {
                    snake.ChangeDirection(direction.Value);
                }
            }

            if (!isPaused)
                Update();

            Render();
            Thread.Sleep(updateInterval);
        }

        _gameOutput.DisplayGameOver(new GameState()
        {
            Snake = snake,
            Score = score
        });
    }

    private void SpawnFood()
    {
        var position = gameBoard.GetRandomPositionExcluding(snake.Body);
        do
        {
            position = gameBoard.GetRandomPositionExcluding(snake.Body);
        } while (position.X == 1 || position.X == gameBoard.Width - 1 || position.Y == 1 || position.Y == gameBoard.Height - 1);
        if (food == null)
        {
            food = new Food(position);
        }
        else
        {
            food.Respawn(position);
        }
    }

    public void Update()
    {
        if (isGameOver) return;

        snake.Move();
        // SpawnFood();

        if (snake.Eat(food))
        {
            SpawnFood();
            score += 10;
        }

        if (snake.CollidesWithSelf() || gameBoard.IsOutOfBounds(snake.Head))
        {
            isGameOver = true;
            return;
        }
    }

    public void Render()
    {
        if (!isGameOver)
        {
            _gameOutput.Display(new GameState()
            {
                Food = food,
                Snake = snake,
                Score = score,
                IsPaused = isPaused
            });
        }
    }

    public void SetSpeed(int interval) {
        updateInterval = interval;
    }
}
