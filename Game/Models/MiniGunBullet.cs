using System.Numerics;

namespace Game.Models
{
    public class MiniGunBullet : GunBullet
    {
        public MiniGunBullet(Vector2 position, int directionX, int directionY)
            : base(position, directionX, directionY, Constants.MiniGunBulletSize)
        {
        }
    }
}
