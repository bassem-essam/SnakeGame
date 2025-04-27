namespace SnakeGame.Core.Entities
{
    public class GameBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Random random = new Random();


        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsValidPosition(Position position)
        {
            return !IsOutOfBounds(position);
        }

        public bool IsOutOfBounds(Position position)
        {
            return position.X < 1 || position.X > Width ||
                   position.Y < 1 || position.Y > Height;
        }

        public Position GetRandomPosition()
        {
            return new Position(1 + random.Next(Width - 1), 1 + random.Next(Height - 1));
        }

        public Position GetRandomPositionExcluding(IEnumerable<Position> excludedPositions)
        {
            Position randomPosition;
            do
            {
                randomPosition = GetRandomPosition();
            } while (excludedPositions.Contains(randomPosition));

            return randomPosition;
        }
    }
}