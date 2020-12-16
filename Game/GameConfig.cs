using Game.Factory;
using Game.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;

namespace Game
{
    /// <summary>
    /// Конфигурационный класс для настройки глобальных значений
    /// </summary>
    public class GameConfig
    {
        public Player Player { get; set; }

        public string ServerAdress { get; set; }

        public int Port { get; set; }

        // Словарь с настройками
        public static Dictionary<int, GameConfig> Configs = new Dictionary<int, GameConfig>
        {
            [1] = new GameConfig
            {
                Player = GameObjectFactory.CreatePlayer(
                    new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                    new Vector2(2, 0.5f),
                    ComponentsFactory.CreateControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C)),
                ServerAdress = "127.0.0.1",
                //ServerAdress = "192.168.100.7",
                Port = 8080

            },
            [2] = new GameConfig
            {
                Player = GameObjectFactory.CreatePlayer(
                    new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    new Vector2(-2, 0.5f),
                    ComponentsFactory.CreateControlSettings(Keys.NumPad8, Keys.NumPad2, Keys.NumPad4, Keys.NumPad6, Keys.NumPad0, Keys.NumPad1)),
                ServerAdress = "127.0.0.2",
                //ServerAdress = "192.168.100.8",
                Port = 8000
            }
        };
    }
}
