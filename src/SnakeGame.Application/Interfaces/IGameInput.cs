using SnakeGame.Core.Entities;

namespace SnakeGame.Application.Interfaces;

public interface IGameInput
{
    bool HasInput();
    Direction? GetDirection();
    bool ToggledPause();
    bool RequestedExit();
}