namespace SnakeGame.Core.Entities;

public class Snake
{
    private readonly List<Position> _body;
    private Direction _currentDirection;
    private Direction _nextDirection;

    public IReadOnlyList<Position> Body => _body.AsReadOnly();
    public Direction CurrentDirection => _currentDirection;
    public Direction NextDirection => _nextDirection;
    public bool IsAlive { get; private set; } = true;

    public Position Head => _body[0];
    public IEnumerable<Position> Tail => _body.Skip(1);

    public Snake(Position initialPosition, Direction initialDirection = Direction.Right)
    {
        _currentDirection = initialDirection;
        _nextDirection = initialDirection;
        _body = new List<Position>() { initialPosition };
    }

    public void ChangeDirection(Direction newDirection)
    {
        if (!newDirection.IsOpposite(_currentDirection))
        {
            _nextDirection = newDirection;
        }
    }

    public void Move()
    {
        _currentDirection = _nextDirection;
        var newHead = Head.Add(_currentDirection);

        // Move snake by adding new head and removing tail
        _body.Insert(0, newHead);
        _body.RemoveAt(_body.Count - 1);
    }

    public void Grow()
    {
        // Snake grows by keeping its tail when moving
        var newHead = Head.Add(_currentDirection);
        _body.Insert(0, newHead);
    }

    public bool CollidesWith(Position position)
    {
        return Head == position;
    }

    public bool CollidesWithSelf()
    {
        foreach (var segment in Tail) {
            if (CollidesWith(segment)) {
                return true;
            }
        }

        return false;
    }

    public bool Eat(Food food) {
        if (Head == food.Position) {
            Grow();
            return true;
        }
        
        return false;
    }

}
