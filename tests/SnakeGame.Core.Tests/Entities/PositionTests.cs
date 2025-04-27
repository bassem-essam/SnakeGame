using Xunit;
using SnakeGame.Core.Entities;

namespace SnakeGame.Core.Tests.Entities;

public class PositionTests
{
    [Fact]
    public void Constructor_WithNegativeX_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Position(-1, 0));
    }

    [Fact]
    public void Constructor_WithNegativeY_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Position(0, -1));
    }

    [Fact]
    public void Constructor_WithValidCoordinates_SetsProperties()
    {
        var position = new Position(5, 10);
        Assert.Equal(5, position.X);
        Assert.Equal(10, position.Y);
    }

    [Fact]
    public void Add_WithDirection_ReturnsCorrectPosition()
    {
        var position = new Position(1, 1);
        
        var rightPosition = position.Add(Direction.Right);
        var leftPosition = position.Add(Direction.Left);
        var upPosition = position.Add(Direction.Up);
        var downPosition = position.Add(Direction.Down);

        Assert.Equal(rightPosition, new Position(2, 1));
        Assert.Equal(leftPosition, new Position(0, 1));
        Assert.Equal(upPosition, new Position(1, 0));
        Assert.Equal(downPosition, new Position(1, 2));
        
    }

    [Fact]
    public void Equals_WithSameValues_ReturnsTrue()
    {
        var pos1 = new Position(1, 2);
        var pos2 = new Position(1, 2);
        
        Assert.True(pos1.Equals(pos2));
    }

    [Fact]
    public void Equals_WithDifferentValues_ReturnsFalse()
    {
        var pos1 = new Position(1, 2);
        var pos2 = new Position(3, 4);
        
        Assert.False(pos1.Equals(pos2));
    }

    [Fact]
    public void GetHashCode_ForEqualPositions_ReturnsSameValue()
    {
        var pos1 = new Position(1, 2);
        var pos2 = new Position(1, 2);
        
        Assert.Equal(pos1.GetHashCode(), pos2.GetHashCode());
    }

    [Fact]
    public void EqualityOperator_WithSameValues_ReturnsTrue()
    {
        var pos1 = new Position(1, 2);
        var pos2 = new Position(1, 2);
        
        Assert.True(pos1 == pos2);
    }

    [Fact]
    public void InequalityOperator_WithDifferentValues_ReturnsTrue()
    {
        var pos1 = new Position(1, 2);
        var pos2 = new Position(3, 4);
        
        Assert.True(pos1 != pos2);

        pos1 = new Position(1, 2);
        pos2 = new Position(1, 4);
        
        Assert.True(pos1 != pos2);

        pos1 = new Position(1, 2);
        pos2 = new Position(3, 2);
        
        Assert.True(pos1 != pos2);
    }
}
