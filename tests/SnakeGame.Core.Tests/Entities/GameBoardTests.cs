using SnakeGame.Core.Entities;

namespace SnakeGame.Domain.Tests.Entities
{
    public class GameBoardTests
    {
        [Fact]
        public void Constructor_SetsWidthAndHeight()
        {
            // Arrange & Act
            var gameBoard = new GameBoard(20, 15);

            // Assert
            Assert.Equal(20, gameBoard.Width);
            Assert.Equal(15, gameBoard.Height);
        }

        [Theory]
        [InlineData(20, 5, true)]
        [InlineData(5, 15, true)]
        [InlineData(0, 0, false)]
        [InlineData(19, 14, false)]
        [InlineData(10, 10, false)]
        public void IsOutOfBounds_ReturnsCorrectResult(int x, int y, bool expected)
        {
            // Arrange
            var gameBoard = new GameBoard(20, 15);
            var position = new Position(x, y);

            // Act
            var result = gameBoard.IsOutOfBounds(position);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetRandomPosition_ReturnsPositionWithinBounds()
        {
            // Arrange
            var gameBoard = new GameBoard(20, 15);

            // Act
            var position = gameBoard.GetRandomPosition();

            // Assert
            Assert.InRange(position.X, 0, gameBoard.Width - 1);
            Assert.InRange(position.Y, 0, gameBoard.Height - 1);
        }

        [Fact]
        public void GetRandomPositionExcluding_ReturnsPositionNotInExcludedList()
        {
            // Arrange
            var gameBoard = new GameBoard(5, 5); // Small board for testing
            
            // Create a list of excluded positions that covers most but not all of the board
            var excludedPositions = new List<Position>();
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    excludedPositions.Add(new Position(x, y));
                }
            }
            // This leaves only positions with x=4 available

            // Act
            var result = gameBoard.GetRandomPositionExcluding(excludedPositions);

            // Assert
            Assert.Equal(4, result.X); // Only x=4 positions are available
            Assert.InRange(result.Y, 0, 4); // Y can be 0-4
            // Assert.DoesNotContain(excludedPositions, result); // Result must not be in excluded list
        }
    }
}
