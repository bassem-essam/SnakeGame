using SnakeGame.Application;
using SnakeGame.ConsoleUI.Adapters;
using SnakeGame.ConsoleUI.Framework;


ConsoleGameOutputAdapter gameOutput = new ConsoleGameOutputAdapter();
if (args.Contains("-full")) {
    gameOutput = new ConsoleGameOutputAdapter(Screen.CreateFullScreen());
}

ConsoleGameInputAdapter gameInput =  new ConsoleGameInputAdapter();
// gameOutput.SetBackgroundColor(Color.BgLightBlue);

Game game = new Game(gameOutput, gameInput);

// game.SetSpeed(50);

game.Run();