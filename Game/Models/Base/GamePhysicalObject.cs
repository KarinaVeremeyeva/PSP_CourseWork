using System.Numerics;

namespace Game.Models.Base
{
    /// <summary>
    /// Класс для создания физических игровых объектов, который содержит свойство брони, конструктор с параметрами цвета, позиции и размера
    /// </summary>
    public class GamePhysicalObject : GameObject
    {
        protected float Armor { get; set; }

        public GamePhysicalObject(Vector2 position, Vector4 color, float size, int armor)
            : base(position, color, size)
        {
            Armor = armor;
        }

        // для получения урона
        public void GetDamage(float damage = 1)
        {
            Armor -= damage;
        }

        // для определения уничтожен ли объект
        public virtual bool IsDeadObject()
        {
            return Armor <= 0;
        }
    }
}
