using SnakeGame.Core.Entities;

namespace SnakeGame.Core.Tests.Entities;

public class SnakeTests
{
    [Fact]
    public void CollidesWith_WithCollidingPosition_ReturnsTrue()
    {
        var snake = new Snake(new Position(1, 2));
        var position = new Position(1, 2);
        Assert.True(snake.CollidesWith(position));

        snake.Move();
        position = new Position(2, 2);
        Assert.True(snake.CollidesWith(position));
    }

    [Fact]
    public void CollidesWith_WithNonCollidingPosition_ReturnsFalse()
    {
        var snake = new Snake(new Position(1, 2));
        var position = new Position(1, 2);

        Assert.False(snake.CollidesWith(position.Add(Direction.Up)));
        Assert.False(snake.CollidesWith(position.Add(Direction.Down)));
        Assert.False(snake.CollidesWith(position.Add(Direction.Left)));
        Assert.False(snake.CollidesWith(position.Add(Direction.Right)));

        // Tried adding some extreme positions, I am not sure if this is needed
        // but it is a good idea to have some edge cases
        Assert.False(snake.CollidesWith(new Position(0, 0)));
        Assert.False(snake.CollidesWith(new Position(int.MaxValue, int.MaxValue)));
    }


    [Fact]
    public void CollidesWithSelf_WithCollidingPosition_ReturnsTrue()
    {

        var snake = new Snake(new Position(1, 1));
        snake.Grow();
        snake.Grow();
        snake.Grow();
        snake.Grow();
        snake.ChangeDirection(Direction.Down);
        snake.Move();
        snake.ChangeDirection(Direction.Left);
        snake.Move();
        snake.ChangeDirection(Direction.Up);
        snake.Move();
        
        Assert.True(snake.CollidesWithSelf());
    }

    [Theory]
    [InlineData(2, 2, 1, 2, Direction.Left)]
    [InlineData(2, 2, 3, 2, Direction.Right)]
    [InlineData(2, 2, 2, 3, Direction.Down)]
    [InlineData(2, 2, 2, 1, Direction.Up)]
    public void Grow_GrowsSnakeInCorrectDirection(int headX, int headY, int newHeadX, int newHeadY, Direction direction)
    {
        var snake = new Snake(new Position(headX, headY), direction);
        snake.Grow();
        Assert.Equal(2, snake.Body.Count);

        var head = snake.Body[0];
        var tail = snake.Body[1];
        Assert.Equal(new Position(newHeadX, newHeadY), head);
        Assert.Equal(new Position(headX, headY), tail);
    }

    [Theory]
    [InlineData(Direction.Right, 3, 2)]
    [InlineData(Direction.Left, 1, 2)]
    [InlineData(Direction.Up, 2, 1)]
    public void Move_MovesSnakeInCorrectDirection(Direction direction, int headX, int headY)
    {
        var snake = new Snake(new Position(2, 2), direction);
        snake.Move();
        Assert.Equal(new Position(headX, headY), snake.Body[0]);
    }
}