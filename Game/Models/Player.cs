using Game.Factory;
using Game.Models.Base;
using Game.Models.ModelComponents;
using System.Numerics;

namespace Game.Models
{
    /// <summary>
    /// Класс игрока, конструктор которого содержит цвет, позицию, настройки клавиш
    /// </summary>
    public class Player: MovableGameObject
    {
        public ControlSettings Control { get; }

        public int FuelCount { get; set; }

        public int GunBulletCount { get; set; }

        public int MiniGunBulletCount { get; set; }

        public Player(Vector4 color, Vector2 position, ControlSettings controlSettings)
            : base(position, color, Constants.PlayerSize, Constants.PlayerHealth, Constants.PlayerSpeed)
        {
            Control = controlSettings;

            GunBulletCount = Constants.PlayerGunBulletCount;

            MiniGunBulletCount = Constants.PlayerMiniGunBulletCount;

            FuelCount = Constants.PlayerFuelCount;
        }

        // выстрел мнгновенным снарядом
        public virtual MiniGunBullet MiniGunFire()
        {
            if (MiniGunBulletCount > 0)
            {
                MiniGunBulletCount--;

                return GameObjectFactory.CreateMiniGunBullet(this);
            }

            return null;
        }

        // выстрел снарядом
        public virtual GunBullet GunFire()
        {
            if (GunBulletCount > 0)
            {
                GunBulletCount--;

                return GameObjectFactory.CreateGunBullet(this);
            }

            return null;
        }

        // движение игрока
        public override void Move()
        {
            if (FuelCount > 0)
            {
                FuelCount--;

                base.Move();
            }
            else
            {
                OldPosition = new Vector2(
                    OldPosition.X - (NewPosition.X - Position.X),
                    OldPosition.Y - (NewPosition.Y - Position.Y));

                NewPosition = Position;
            }
        }

        // строковое представление характеристик игрока
        public override string ToString()
        {
            return
                $"Armor: {Armor}\n" +
                $"Fuel: {FuelCount}\n" +
                $"Gun bullets count: {GunBulletCount}\n" +
                $"Minigun bullets count: {MiniGunBulletCount}\n" +
                $"Position: {Position.X:f2} {Position.Y:f2}\n" +
                $"PositionNew: {NewPosition.X:f2} {NewPosition.Y:f2}";
        }
    }
}
