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
using System.Net.Sockets;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

namespace Game
{
    public partial class Form1 : Form
    {
        private const int port = 8080;

        private readonly GameConfig gameConfig;

        private readonly string enemyServerAddres;

        private readonly GameRepository gameRepository;

        private readonly GameActionService gameActionService;

        private readonly DrawingService drawingService;

        private bool isGameEnd = false;

        public Form1(int applicationId)
        {
            this.gameConfig = GameConfig.Configs[applicationId];
            this.enemyServerAddres = applicationId == 1
                ? GameConfig.Configs[2].ServerAdress
                : GameConfig.Configs[1].ServerAdress;

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
                RunServer();
            });

            Task.Run(() =>
            {
                while (!isGameEnd)
                {
                    glControl1.Invalidate();

                    Thread.Sleep(100);
                }
            });
        }

        /// <summary>
        /// Прослушивает все подключения по порту
        /// </summary>
        public void RunServer()
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse(this.gameConfig.ServerAdress);
                server = new TcpListener(localAddr, port);
                // запуск слушателя
                server.Start();

                while (true)
                {
                    byte[] data = new byte[1024];

                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();

                    // получение данных
                    int bytes = stream.Read(data, 0, data.Length);
                    var responseData = Encoding.UTF8.GetString(data, 0, bytes).ToString();
                    var deserializedKey = JsonConvert.DeserializeObject<Keys>(responseData);

                    gameActionService.ProcessPlayerAction(deserializedKey);

                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }

        /// <summary>
        /// Обработка запросов клиента
        /// </summary>
        public void SendKeyToOpponent(Keys key)
        {
            TcpClient client = new TcpClient();
            client.Connect(this.enemyServerAddres, port);

            NetworkStream stream = client.GetStream();

            var keyToString = JsonConvert.SerializeObject(key);
            var data = Encoding.UTF8.GetBytes(keyToString);

            stream.Write(data, 0, data.Length);

            // Закрываем потоки
            stream.Close();
            client.Close();
        }

        private void CreateAreas()
        {
            gameRepository.GameAreas.Add(new NotFireArea(new Vector2(0, 2)));
            gameRepository.GameAreas.Add(new DecelerationArea(new Vector2(-1, -1)));
            gameRepository.GameAreas.Add(new ImpassableArea(new Vector2(-1.5f, 2)));
        }

        private void CreatePlayers()
        {
            gameRepository.Player1 = GameConfig.Configs[1].Player;
            gameRepository.Player2 = GameConfig.Configs[2].Player;
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
            // Проверка, чтобы не было введено запрещённых действий, например, движение другого игрока
            var currentPlayerControl = this.gameConfig.Player.Control;
            var currentPlayerActions = new Keys[]
            {
                currentPlayerControl.Down,
                currentPlayerControl.Up,
                currentPlayerControl.Left,
                currentPlayerControl.Right,
                currentPlayerControl.Fire,
                currentPlayerControl.FireMiniGun,
            };
            var isKeyValidForThisApplication = currentPlayerActions.Contains(e.KeyCode);
            if (!isKeyValidForThisApplication)
            {
                return;
            }

            // Обработка ввода
            gameActionService.ProcessPlayerAction(e.KeyCode);
            SendKeyToOpponent(e.KeyCode);
            glControl1.Invalidate();
        }
    }
}
