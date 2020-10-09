using Game.Models.Base;
using System.Numerics;
using System;

namespace Game.Models
{
    public class GunBullet : MovableGameObject
    {
        public int DirectionX { get; }

        public int DirectionY { get; }

        public override bool IsDeadObject()
        {
            return base.IsDeadObject() ||
                Math.Abs(Position.X) >= Constants.MaxMapPosition ||
                Math.Abs(Position.Y) >= Constants.MaxMapPosition;
        }

        public GunBullet(
            Vector2 position, int directionX, int directionY,
            float size = Constants.GunBulletSize, float speed = Constants.GunBulletSpeed)
            : base(position, new Vector4(10, 10, 10, 0), size, Constants.BulletHealth, speed)
        {
            DirectionX = directionX;
            DirectionY = directionY;
        }

        public override void Move()
        {
            base.Move();

            UpdateNewCoords(Speed * DirectionX, Speed * DirectionY);
        }
    }
}
