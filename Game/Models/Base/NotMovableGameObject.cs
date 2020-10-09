using System.Numerics;

namespace Game.Models.Base
{
    /// <summary>
    /// Класс для генерации неподвижных объектов
    /// </summary>
    public class NotMovableGameObject : GamePhysicalObject
    {
        public NotMovableGameObject(Vector2 position, Vector4 color, float size, int armor)
            : base(position, color, size, armor)
        {
        }
    }
}
