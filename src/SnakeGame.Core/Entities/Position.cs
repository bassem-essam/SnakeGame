namespace SnakeGame.Core.Entities;

public sealed class Position
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y)
    {
        if (x < 0 || y < 0)
            throw new ArgumentException("Position coordinates cannot be negative");

        X = x;
        Y = y;
    }

    public Position Add(Direction direction)
    {
        Position position = this;
        switch (direction) {
            case Direction.Up:
                return new Position(position.X, position.Y - 1);
            case Direction.Down:
                return new Position(position.X, position.Y + 1);
            case Direction.Left:
                return new Position(position.X - 1, position.Y);
            case Direction.Right:
                return new Position(position.X + 1, position.Y);
            default: throw new ArgumentOutOfRangeException(nameof(direction));
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Position position &&
               X == position.X &&
               Y == position.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Position left, Position right)
    {
        return EqualityComparer<Position>.Default.Equals(left, right);
    }

    public static bool operator !=(Position left, Position right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
