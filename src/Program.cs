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

        Raylib.BeginDrawing();

        Raylib.ClearBackground(screen.BackgroundColor);

        Raylib.DrawText($"{playerLeft.Score}", screenWidth / 4, 20, 50, Color.White);
        Raylib.DrawText($"{playerRight.Score}", screenWidth * 3 / 4, 20, 50, Color.White);

        Raylib.DrawLine(screenWidth / 2, 0, screenWidth / 2, screenHeight, Color.Gray);

        ball.Draw();
        playerLeft.Draw();
        playerRight.Draw();

        Raylib.EndDrawing();

        if (state == GameState.Playing)
        {
            if (ball.SpeedX < 0 && ball.X < screenWidth / 2)
            {
                if (ball.X - ball.Radius <= distanceToEdge)
                {
                    playerRight.ScoreIncrement();
                    ResetGame(ref state, ball, playerLeft, playerRight);

                }
                ball.CheckPlayerCollision(playerLeft.GetRectangle());
            }
            else if (ball.SpeedX > 0 && ball.X >= screenWidth / 2)
            {
                if (ball.X + ball.Radius >= Raylib.GetScreenWidth() - distanceToEdge)
                {
                    playerLeft.ScoreIncrement();
                    ResetGame(ref state, ball, playerLeft, playerRight);
                }
                ball.CheckPlayerCollision(playerRight.GetRectangle());
            }

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