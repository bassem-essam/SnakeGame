namespace SnakeGame.ConsoleUI.Framework;

public class Screen
{
    public int Width { get; set; }
    public int Height { get; set; }

    public string[,] Buffer;

    public Color[,] ColorBuffer;

    public List<string> StatusLines = new List<string>();
    public Color StatusLineColor = Color.Reset;

    public Screen(int width, int height, int statusLinesCount = 1)
    {
        Width = width;
        Height = height;

        Buffer = new string[Height, Width];
        ColorBuffer = new Color[Height, Width];

        for (int i = 0; i < statusLinesCount; i++)
        {
            StatusLines.Add("");
        }

        Clear();
    }

    public void Clear(Color color = Color.Reset)
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                ColorBuffer[row, col] = color;
                Buffer[row, col] = " ";
            }
        }
    }

    public void SetPixel(int x, int y, string c, Color color = Color.Reset)
    {
        ColorBuffer[y - 1, x - 1] = color;
        Buffer[y - 1, x - 1] = c;
    }

    public void SetStatusLine(string statusLine, int index = 0) {
        if (index < 0 || index >= StatusLines.Count)
            throw new ArgumentException("Invalid status line index");

        StatusLines[index] = statusLine;
    }

    public void Update()
    {
        Console.Write(Color.Reset.ToAnsi());
        Console.CursorVisible = false;
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                Console.Write($"{ColorBuffer[row, col].ToAnsi()}{Buffer[row, col]}");
            }

            Console.WriteLine();
        }

        var statusLine = string.Join("\n", StatusLines);
        Console.Write(StatusLineColor.ToAnsi());
        Console.Write(statusLine);
    }

    public static Screen CreateFullScreen(int statusLinesCount = 1) {
        return new Screen(Console.WindowWidth, Console.WindowHeight - statusLinesCount, statusLinesCount);
    }
}