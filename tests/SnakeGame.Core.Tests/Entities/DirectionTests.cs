using SnakeGame.Core.Entities;

namespace SnakeGame.Core.Tests.Entities;

public class DirectionTests
{
    [Theory]
    [InlineData(Direction.Up, Direction.Down)]
    [InlineData(Direction.Down, Direction.Up)]
    [InlineData(Direction.Left, Direction.Right)]
    [InlineData(Direction.Right, Direction.Left)]
    public void IsOpposite_WithOppositeDirection_ReturnsTrue(Direction direction1, Direction direction2)
    {
        Assert.True(direction1.IsOpposite(direction2));
    }

    [Theory]
    [InlineData(Direction.Up, Direction.Left)]
    [InlineData(Direction.Up, Direction.Right)]
    [InlineData(Direction.Down, Direction.Left)]
    [InlineData(Direction.Down, Direction.Right)]
    [InlineData(Direction.Left, Direction.Up)]
    [InlineData(Direction.Left, Direction.Down)]
    [InlineData(Direction.Right, Direction.Up)]
    [InlineData(Direction.Right, Direction.Down)]
    public void IsOpposite_WithNonOppositeDirection_ReturnsFalse(Direction direction1, Direction direction2)
    {
        Assert.False(direction1.IsOpposite(direction2));
    }
}
