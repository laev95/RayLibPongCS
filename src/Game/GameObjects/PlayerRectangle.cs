namespace GameNamespace;

using Raylib_cs;

class PlayerRectangle(bool isLeft, int x, int y, int width, int height, int speed, Color color) : IGameObject
{
    private readonly bool _isLeft = isLeft;
    private readonly int _x = x;
    private int y = y;
    private readonly int _startY = y;
    private readonly int _ySpeed = speed;
    private readonly int _width = width;
    private readonly int _height = height;
    private readonly Color _color = color;

    public int Score { get; private set; } = 0;

    public void Draw()
    {
        Raylib.DrawRectangle(_x, y, _width, _height, _color);
    }

    public void Move()
    {
        if (_isLeft)
        {
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                y -= _ySpeed;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                y += _ySpeed;
            }
        }
        else
        {
            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                y -= _ySpeed;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                y += _ySpeed;
            }
        }

        if (y <= 0)
        {
            y = 0;
        }
        else if (y + _height >= Raylib.GetScreenHeight())
        {
            y = Raylib.GetScreenHeight() - _height;
        }
    }

    public Rectangle GetRectangle()
    {
        return new Rectangle(_x, y, _width, _height);
    }

    public void ScoreIncrement()
    {
        Score++;
    }

    public void Reset()
    {
        y = _startY;
    }
}