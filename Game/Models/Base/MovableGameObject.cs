using Game.Interfaces;
using System.Numerics;

namespace Game.Models.Base
{
    /// <summary>
    /// Класс для генерации подвижных объектов, который содержит свойства старой позиции, новой позиции, скорости игрового объекта
    /// </summary>
    public class MovableGameObject : GamePhysicalObject, IMoveable
    {
        public Vector2 OldPosition { get; set; }

        public Vector2 NewPosition { get; set; }

        public float Speed { get; set; }

        public MovableGameObject(Vector2 position, Vector4 color, float size, int health, float speed)
            : base(position, color, size, health)
        {
            OldPosition = position;
            NewPosition = position;

            Speed = speed;
        }

        // движение
        public virtual void Move()
        {
            OldPosition = Position;
            Position = NewPosition;
        }

        // возвращение новых координат
        public void RollBackNewCoords()
        {
            NewPosition = Position;
        }

        // обновление новых координат
        public virtual void UpdateNewCoords(float x, float y)
        {
            NewPosition = new Vector2(NewPosition.X + x, NewPosition.Y + y);
        }
    }
}
