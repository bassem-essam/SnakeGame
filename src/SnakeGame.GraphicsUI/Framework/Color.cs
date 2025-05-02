namespace SnakeGame.GraphicsUI.Framework;

/// <summary>
/// Represents a color with RGBA components
/// </summary>
public struct Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public Color(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static Color Black => new Color(0, 0, 0);
    public static Color White => new Color(255, 255, 255);
    public static Color Red => new Color(255, 0, 0);
    public static Color Green => new Color(0, 255, 0);
    public static Color Blue => new Color(0, 0, 255);
    public static Color Yellow => new Color(255, 255, 0);

    internal SFML.Graphics.Color ToSFMLColor()
    {
        return new SFML.Graphics.Color(R, G, B, A);
    }
}
