using Raylib_cs;
using GameNamespace;

const int screenWidth = 854;
const int screenHeight = 480;

const int distanceToEdge = 10;
const int rectangleWidth = 15;
const int rectangleHeight = 75;
const int rectangleSpawnX = screenWidth - (distanceToEdge + rectangleWidth);
const int rectangleSpawnY = (screenHeight / 2) - (rectangleHeight / 2);
const int rectangleSpeed = 200;

const int ballRadius = 10;
const int ballSpeedX = 300;
const int ballSpeedY = 300;

const float pauseDuration = 1.0f;

MainLoop();

static void MainLoop()
{
    Screen screen = new(screenWidth, screenHeight, Color.Black);
    screen.InitWindow();

    GameState state = GameState.Playing;
    float pauseTimer = 0f;

    Ball ball = new(screenWidth / 2, screenHeight / 2, ballRadius, ballSpeedX, ballSpeedY, Color.White);
    PlayerRectangle playerLeft = new(true, distanceToEdge, rectangleSpawnY, rectangleWidth, rectangleHeight, rectangleSpeed, Color.White);
    PlayerRectangle playerRight = new(false, rectangleSpawnX, rectangleSpawnY, rectangleWidth, rectangleHeight, rectangleSpeed, Color.White);

    Raylib.SetTargetFPS(60);

    while (!Raylib.WindowShouldClose())
    {
        float dt = Raylib.GetFrameTime();

        if (state == GameState.Playing)
        {
            ball.Move(dt);
            playerLeft.Move(dt);
            playerRight.Move(dt);
        }
        else if (state == GameState.Paused)
        {
            pauseTimer += dt;
            if (pauseTimer >= pauseDuration)
            {
                state = GameState.Playing;
                pauseTimer = 0f;
            }
        }

        Raylib.BeginDrawing();

        Raylib.ClearBackground(screen.BackgroundColor);

        Raylib.DrawText($"{playerLeft.Score}", screenWidth / 4, 20, 50, Color.White);
        Raylib.DrawText($"{playerRight.Score}", screenWidth * 3 / 4, 20, 50, Color.White);

        Raylib.DrawLine(screenWidth / 2, 0, screenWidth / 2, screenHeight, Color.Gray);

        ball.Draw();
        playerLeft.Draw();
        playerRight.Draw();

        if (ball.SpeedX < 0 && ball.X < screenWidth / 2)
        {
            ball.CheckPlayerCollision(playerLeft.GetRectangle());
            if (ball.X - ball.Radius <= 0)
            {
                playerRight.ScoreIncrement();
                ResetGame(ref state, ball, playerLeft, playerRight);

            }
        }
        else if (ball.SpeedX > 0 && ball.X >= screenWidth / 2)
        {
            ball.CheckPlayerCollision(playerRight.GetRectangle());
            if (ball.X + ball.Radius >= Raylib.GetScreenWidth())
            {
                playerLeft.ScoreIncrement();
                ResetGame(ref state, ball, playerLeft, playerRight);
            }
        }

        Raylib.EndDrawing();
    }
    Raylib.CloseWindow();
}

static void ResetGame(ref GameState state, Ball ball, PlayerRectangle playerLeft, PlayerRectangle playerRight)
{
    ball.Reset();
    playerLeft.Reset();
    playerRight.Reset();

    state = GameState.Paused;
}