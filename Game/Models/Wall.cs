using Game.Models.Base;
using System.Numerics;

namespace Game.Models
{
    /// <summary>
    /// класс стена, конструктор которого содержит позицию, размер, броню
    /// </summary>
    public class Wall : NotMovableGameObject
    {
        public Wall(Vector2 position, float size, int armor)
            : base(position, new Vector4(1, 0.5f, 0, 1), size, armor)
        {
        }
    }
}
