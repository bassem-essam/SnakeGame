using SnakeGame.Application;
using SnakeGame.GraphicsUI.Adapters;
using SnakeGame.GraphicsUI.Framework;

SFMLGameAdapter gameAdapter;
if (args.Contains("-full"))
{
    gameAdapter = new SFMLGameAdapter(Screen.CreateFullScreen());
}
else
{
    gameAdapter = new SFMLGameAdapter(20, 20);
}

var game = new Game(gameAdapter, gameAdapter);

// game.SetSpeed(50);

// Run game loop
game.Run();
