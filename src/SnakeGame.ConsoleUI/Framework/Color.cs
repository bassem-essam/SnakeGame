namespace SnakeGame.ConsoleUI.Framework;

public enum Color {
    Reset,
    Black,
    Blue,
    Green,
    Cyan,
    Red,
    Magenta,
    Brown,
    LightGray,
    DarkGray,
    LightBlue,
    LightGreen,
    LightCyan,
    LightRed,
    LightMagenta,
    Yellow,
    White,
    BgBlack,
    BgWhite,
    BgGreen,
    BgRed
}

public static class ColorExtensions {
    public static string ToAnsi(this Color color) {
        switch (color) {
            case Color.Black: return "\u001b[30m";
            case Color.Blue: return "\u001b[34m";
            case Color.Green: return "\u001b[32m";
            case Color.Cyan: return "\u001b[36m";
            case Color.Red: return "\u001b[31m";
            case Color.Magenta: return "\u001b[35m";
            case Color.Brown: return "\u001b[33m";
            case Color.LightGray: return "\u001b[37m";
            case Color.DarkGray: return "\u001b[90m";
            case Color.LightBlue: return "\u001b[94m";
            case Color.LightGreen: return "\u001b[92m";
            case Color.LightCyan: return "\u001b[96m";
            case Color.LightRed: return "\u001b[91m";
            case Color.LightMagenta: return "\u001b[95m";
            case Color.Yellow: return "\u001b[93m";
            case Color.White: return "\u001b[97m";
            case Color.Reset: return "\u001b[0m";
            case Color.BgBlack: return "\u001b[40m";
            case Color.BgRed: return "\u001b[41m";
            case Color.BgGreen: return "\u001b[42m";
            case Color.BgWhite: return "\u001b[47m";

            default: return "";
        }
    }
}