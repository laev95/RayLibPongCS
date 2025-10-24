namespace GameNamespace;

using Raylib_cs;

struct Screen(int width, int height, Color backgroundColor = default)
{
    public int Width { get; private set; } = width;
    public int Height { get; private set; } = height;
    public Color BackgroundColor { get; private set; } = backgroundColor;

    public readonly void InitWindow()
    {
        Raylib.InitWindow(Width, Height, "PONG");
    }
}