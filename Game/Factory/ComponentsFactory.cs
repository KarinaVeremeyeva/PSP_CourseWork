using Game.Models.ModelComponents;
using System.Windows.Forms;

namespace Game.Factory
{
    /// <summary>
    /// Класс генерирует игровое поле и управление клавиатурой
    /// </summary>
    public static class ComponentsFactory
    {
        // игровое поле
        private static readonly GameRepository gameRepository;

        static ComponentsFactory()
        {
            gameRepository = new GameRepository();
        }

        // настройки клавиатуры
        public static ControlSettings CreateControlSettings(Keys up, Keys down, Keys left, Keys right, Keys fire, Keys fireMiniGun)
        {
            return new ControlSettings(up, down, left, right, fire, fireMiniGun);
        }

        // создание игрового поля
        public static GameRepository CreateGameRepository()
        {
            return gameRepository ?? new GameRepository();
        }
    }
}
