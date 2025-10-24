namespace GameNamespace;

using System.Numerics;
using Raylib_cs;

class Ball(int x, int y, int radius, int speedX, int speedY, Color color = default) : IGameObject
{
    private readonly int _startX = x;
    private readonly int _startY = y;

    public float X { get; private set; } = x;
    public float Y { get; private set; } = y;
    public int Radius { get; private set; } = radius;
    public int SpeedX { get; private set; } = speedX;
    public int SpeedY { get; private set; } = speedY;
    public Color Color { get; private set; } = color;

    public void Move(float dt)
    {
        if (Y - Radius <= 0 || Y + Radius >= Raylib.GetScreenHeight())
        {
            SpeedY *= -1;
        }

        X += SpeedX * dt;
        Y += SpeedY * dt;
    }

    public void Draw()
    {
        Raylib.DrawCircle((int) X, (int) Y, Radius, Color);
    }

    public void CheckPlayerCollision(Rectangle player)
    {
        if (Raylib.CheckCollisionCircleRec(new Vector2(X, Y), Radius, player))
        {
            SpeedX *= -1;
        }
    }

    public void Reset()
    {
        X = _startX;
        Y = _startY;

        int[] possibleDirections = [-1, 1];

        SpeedX *= possibleDirections[Raylib.GetRandomValue(0, 1)];
        SpeedY *= possibleDirections[Raylib.GetRandomValue(0, 1)];
    }
}