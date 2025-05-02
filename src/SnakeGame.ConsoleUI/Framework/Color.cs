namespace SnakeGame.ConsoleUI.Framework;

public enum Color {
    Reset,
    BgBlack,
    BgRed,
    BgGreen,
    BgYellow,
    BgBlue,
    BgMagenta,
    BgCyan,
    BgWhite,
    BgLightBlack,
    BgLightRed,
    BgLightGreen,
    BgLightYello,
    BgLightBlue,
    BgLightMagenta,
    BgLightCyan,
    BgLightWhite,
}

public static class ColorExtensions {
    public static string ToAnsi(this Color color) {
        switch (color) {
            case Color.Reset: return "\u001b[0m";
            case Color.BgBlack: return "\u001b[40m";
            case Color.BgRed: return "\u001b[41m";
            case Color.BgGreen: return "\u001b[42m";
            case Color.BgYellow: return "\u001b[43m";
            case Color.BgBlue: return "\u001b[44m";
            case Color.BgMagenta: return "\u001b[45m";
            case Color.BgCyan: return "\u001b[46m";
            case Color.BgWhite: return "\u001b[47m";
            case Color.BgLightBlack: return "\u001b[100m";
            case Color.BgLightRed: return "\u001b[101m";
            case Color.BgLightGreen: return "\u001b[102m";
            case Color.BgLightYello: return "\u001b[103m";
            case Color.BgLightBlue: return "\u001b[104m";
            case Color.BgLightMagenta: return "\u001b[105m";
            case Color.BgLightCyan: return "\u001b[106m";
            case Color.BgLightWhite: return "\u001b[107m";

            default: return "";
        }
    }
}