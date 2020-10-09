namespace Game.Models.Decorators
{
    public class NotFireDecorator : PlayerBaseDecorator
    {
        public NotFireDecorator(Player player)
            : base(player)
        {
        }

        public override GunBullet GunFire()
        {
            return null;
        }

        public override MiniGunBullet MiniGunFire()
        {
            return null;
        }
    }
}
