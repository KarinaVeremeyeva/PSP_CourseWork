using System.Numerics;

namespace Game.Models
{
    /// <summary>
    /// Класс для мгновенного снаряда, конструктор содержит позицию и координаты
    /// </summary>
    public class MiniGunBullet : GunBullet
    {
        public MiniGunBullet(Vector2 position, int directionX, int directionY)
            : base(position, directionX, directionY, Constants.MiniGunBulletSize)
        {
        }
    }
}
