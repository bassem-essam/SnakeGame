using SnakeGame.Application;
using SnakeGame.ConsoleUI.Adapters;
using SnakeGame.ConsoleUI.Framework;


GameOutput gameOutput = new GameOutput();
if (args.Contains("-full")) {
    gameOutput = new GameOutput(Screen.CreateFullScreen());
}

GameInput gameInput =  new GameInput();
gameOutput.SetBackgroundColor(Color.BgWhite);
Game game = new Game(gameOutput, gameInput);
game.SetSpeed(50);
game.Run();