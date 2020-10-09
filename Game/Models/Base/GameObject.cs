using System.Numerics;

namespace Game.Models.Base
{
    /// <summary>
    /// Абстрактный класс для генерации игровых объектов, который содержит свойства позиции, 
    /// цвета и размера игрового объекта, конструктор с параметрами цвета, позиции и размера
    /// </summary>
    public abstract class GameObject
    {
        public float Size { get; set; }

        public Vector2 Position { get; set; }

        public Vector4 Color { get; set; }

        public GameObject(Vector2 position, Vector4 color, float size)
        {
            Position = position;
            Color = color;
            Size = size;
        }
    }
}