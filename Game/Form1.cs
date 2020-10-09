using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Game.Factory;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;
using Game.Services;
using System.Threading.Tasks;
using System.Threading;
using Game.Models.Areas;

namespace Game
{
    public partial class Form1 : Form
    {
        private readonly GameRepository gameRepository;

        private readonly GameActionService gameActionService;

        private readonly DrawingService drawingService;

        private bool isGameEnd = false;

        public Form1()
        {
            // Создание сервисов и репозиториев
            gameRepository = ComponentsFactory.CreateGameRepository();

            gameActionService = ServiceFactory.CreateGameActionService();

            CreateAreas();

            CreatePlayers();

            CreateWalls();

            drawingService = ServiceFactory.CreateDrawingService();

            InitializeComponent();

            Task.Run(() =>
            {
                while (!isGameEnd)
                {
                    glControl1.Invalidate();

                    Thread.Sleep(100);
                }
            });
        }

        private void CreateAreas()
        {
            gameRepository.GameAreas.Add(new NotFireArea(new Vector2(0, 2)));
            gameRepository.GameAreas.Add(new DecelerationArea(new Vector2(-1, -1)));
            gameRepository.GameAreas.Add(new ImpassableArea(new Vector2(-1.5f, 2)));
        }

        private void CreatePlayers()
        {
            gameRepository.Player1 = GameObjectFactory.CreatePlayer(
                new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector2(2, 0.5f),
                ComponentsFactory.CreateControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));
            gameRepository.Player2 = GameObjectFactory.CreatePlayer(
                new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector2(-2, 0.5f),
                ComponentsFactory.CreateControlSettings(Keys.NumPad8, Keys.NumPad2, Keys.NumPad4, Keys.NumPad6, Keys.NumPad0, Keys.NumPad1));
        }

        private void CreateWalls()
        {
            for (var x = Constants.BorderWallLeftTopX; x >= Constants.BorderWallRigftBottomX; x -= Constants.BorderWallSize)
            {
                gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                    new Vector2(x, Constants.BorderWallLeftTopY),
                    Constants.BorderWallSize,
                    Constants.BorderWallHealth));

                gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                    new Vector2(x, Constants.BorderWallRigftBottomY),
                    Constants.BorderWallSize,
                    Constants.BorderWallHealth));
            }

            for (var y = Constants.BorderWallLeftTopY; y >= Constants.BorderWallRigftBottomY; y -= Constants.BorderWallSize)
            {
                gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                    new Vector2(Constants.BorderWallLeftTopX, y),
                    Constants.BorderWallSize,
                    Constants.BorderWallHealth));

                gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                    new Vector2(Constants.BorderWallRigftBottomX, y),
                    Constants.BorderWallSize,
                    Constants.BorderWallHealth));
            }

            gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                new Vector2(1, 1.5f),
                Constants.WallSize,
                Constants.WallHealth));

            gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                new Vector2(2, 1.5f),
                Constants.WallSize,
                Constants.WallHealth));

            gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                new Vector2(1, 0f),
                Constants.WallSize,
                Constants.WallHealth));

            gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                new Vector2(-2, 0f),
                Constants.WallSize,
                Constants.WallHealth));

            gameRepository.Walls.Add(GameObjectFactory.CreateWall(
                new Vector2(2, 0f),
                Constants.WallSize,
                Constants.WallHealth));
        }

        private void GlControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.SkyBlue);
        }

        private void GlControl1_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl1.Size.Width, glControl1.Size.Height);
            GL.MatrixMode(MatrixMode.Projection);
            var projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 2, 
                glControl1.Size.Width / (float)glControl1.Size.Height, 
                1.0f, 
                64.0f);
            GL.LoadMatrix(ref projection);
        }

        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            player1Info.Text = gameRepository.Player1.ToString();
            player2Info.Text = gameRepository.Player2.ToString();

            DrawObjects();

            // Проверка столкновений пуль с чем-нибудь и обработка
            gameActionService.ProcessBulletsCollisions();

            // Уничтожение мёртвых объектов
            gameActionService.DisposeDeadObjects();

            // Обработка полёта пули
            gameActionService.ProcessBulletsMoving();

            // Проверка условий победы
            if (gameActionService.IsFirstPlayerWon())
            {
                glControl1.Dispose();
                MessageBox.Show("First player won");
                isGameEnd = true;
                return;
            }

            if (gameActionService.IsSecondPlayerWon())
            {
                glControl1.Dispose();
                MessageBox.Show("Second player won");
                isGameEnd = true;
                return;
            }

            glControl1.SwapBuffers();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();
        }

        private void DrawObjects()
        {
            foreach (var gameObject in gameRepository.GetAllGameObjects())
            {
                drawingService.DrawObject(gameObject);
            }
        }

        private void GlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка ввода
            gameActionService.ProcessPlayerAction(e.KeyCode);
            glControl1.Invalidate();
        }
    }
}
