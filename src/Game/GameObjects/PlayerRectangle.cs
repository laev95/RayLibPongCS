namespace GameNamespace;

using Raylib_cs;

class PlayerRectangle(bool isLeft, int x, int y, int width, int height, int speed, Color color) : IGameObject
{
    private readonly bool _isLeft = isLeft;
    private readonly float _x = x;
    private float _y = y;
    private readonly int _startY = y;
    private readonly int _ySpeed = speed;
    private readonly int _width = width;
    private readonly int _height = height;
    private readonly Color _color = color;

    public int Score { get; private set; } = 0;

    public void Draw()
    {
        Raylib.DrawRectangle((int ) _x, (int) _y, _width, _height, _color);
    }

    public void Move(float dt)
    {
        if (_isLeft)
        {
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                _y -= _ySpeed * dt;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                _y += _ySpeed * dt;
            }
        }
        else
        {
            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                _y -= _ySpeed * dt;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                _y += _ySpeed * dt;
            }
        }

        if (_y <= 0)
        {
            _y = 0;
        }
        else if (_y + _height >= Raylib.GetScreenHeight())
        {
            _y = Raylib.GetScreenHeight() - _height;
        }
    }

    public Rectangle GetRectangle()
    {
        return new Rectangle(_x, _y, _width, _height);
    }

    public void ScoreIncrement()
    {
        Score++;
    }

    public void Reset()
    {
        _y = _startY;
    }
}