namespace SnakeGame.Core.Entities
{
    public class Food
    {
        public Position Position { get; private set; }

        public Food(Position position)
        {
            Position = position;
        }

        public void Respawn(Position newPosition)
        {
            Position = newPosition;
        }
    }
}
