using System.Numerics;

namespace Game.Models.Base
{
    /// <summary>
    /// класс для создания территорий, содержащий кнострутор с параметрами позиции и цвета
    /// </summary>
    public class GameAreaObject : GameObject
    {
        public GameAreaObject(Vector2 position, Vector4 color)
            : base(position, color, Constants.AreaSize)
        {
        }
    }
}
