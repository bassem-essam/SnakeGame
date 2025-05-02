using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SnakeGame.GraphicsUI
{
    namespace Framework
    {
        /// <summary>
        /// Provides a convenient abstraction over SFML for drawing simple graphics
        /// </summary>
        public class Screen : IDisposable
        {
            private RenderWindow _window;
            private Font _font;
            public int Width;
            public int Height;
            private readonly int _pixelSize;
            private readonly int _statusLineHeight;
            private string _statusText;
            private RectangleShape[,] _pixels;
            private bool _isDisposed;
            private bool isMaximized = false;
            public bool IsOpen => _window.IsOpen;

            public Screen(int width, int height, int pixelSize = 20, string title = "Snake Game")
            {
                Width = width;
                Height = height;
                _pixelSize = pixelSize;
                _statusLineHeight = 30;
                _statusText = string.Empty;

                // Create the SFML window
                _window = new RenderWindow(
                    new VideoMode((uint)(width * pixelSize), (uint)(height * pixelSize + _statusLineHeight)),
                    title,
                    Styles.Titlebar | Styles.Close
                );

                // Set window properties
                _window.SetVerticalSyncEnabled(true);
                _window.Closed += (s, e) => _window.Close();

                if (isMaximized)
                {
                    _window.Position = new Vector2i(0, 0);  
                }

                // Load font for status text
                try
                {
                    var projectDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
                    _font = new Font(Path.Combine(projectDirectory, "arial.ttf"));
                }
                catch (Exception)
                {
                    // If font loading fails, try to find a system font
                    try
                    {
                        string systemFontsPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                        _font = new Font(Path.Combine(systemFontsPath, "arial.ttf"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to load font: {ex.Message}");
                        // Continue without font - status line won't work but game can still run
                    }
                }

                // Initialize pixel grid
                _pixels = new RectangleShape[width, height];
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        _pixels[x, y] = new RectangleShape(new Vector2f(_pixelSize - 1, _pixelSize - 1))
                        {
                            Position = new Vector2f(x * _pixelSize, y * _pixelSize),
                            FillColor = SFML.Graphics.Color.Black
                        };
                    }
                }
            }

            public void SetPixel(int x, int y, Color color)
            {
                if (x >= 1 && x <= Width && y >= 1 && y <= Height)
                {
                    _pixels[x - 1, y - 1].FillColor = color.ToSFMLColor();
                }
            }

            public void Clear(Color color = default)
            {
                SFML.Graphics.Color sfmlColor = color.ToSFMLColor();
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        _pixels[x, y].FillColor = sfmlColor;
                    }
                }
            }

            public static Screen CreateFullScreen(string title = "Snake Game - Fullscreen")
            {
                // Get desktop mode
                VideoMode desktop = VideoMode.DesktopMode;

                // Calculate how many units we can fit
                int pixelSize = 20;
                int statusHeight = 30;

                // Calculate width and height in game units
                int width = (int)(desktop.Width * 0.9) / pixelSize;
                int height = (int)(desktop.Height * 0.9 - statusHeight) / pixelSize;

                return new Screen(width, height, pixelSize, title);
            }


            public void SetStatusLine(string text)
            {
                _statusText = text;
            }

            public void Update()
            {
                _window.DispatchEvents();
                _window.Clear(SFML.Graphics.Color.Black);

                // Draw all pixels
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        _window.Draw(_pixels[x, y]);
                    }
                }

                // Draw status line if font loaded successfully
                if (_font != null && !string.IsNullOrEmpty(_statusText))
                {
                    Text statusText = new Text(_statusText, _font, 16)
                    {
                        Position = new Vector2f(10, Height * _pixelSize + 5),
                        FillColor = SFML.Graphics.Color.White
                    };
                    _window.Draw(statusText);
                }

                _window.Display();
            }

            public Keyboard.Key? PollKeyboard()
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) return Keyboard.Key.Up;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) return Keyboard.Key.Down;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) return Keyboard.Key.Left;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) return Keyboard.Key.Right;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) return Keyboard.Key.Space;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) return Keyboard.Key.Escape;

                return null;
            }

            public void Dispose()
            {
                if (_isDisposed) return;

                _window.Dispose();
                _font?.Dispose();

                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        _pixels[x, y]?.Dispose();
                    }
                }

                _isDisposed = true;
            }
        }
    }
}