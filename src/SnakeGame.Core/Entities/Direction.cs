namespace SnakeGame.Core.Entities
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static bool IsOpposite(this Direction current, Direction newDirection)
        {
            return (current == Direction.Up && newDirection == Direction.Down) ||
                   (current == Direction.Down && newDirection == Direction.Up) ||
                   (current == Direction.Left && newDirection == Direction.Right) ||
                   (current == Direction.Right && newDirection == Direction.Left);
        }
    }
}
