namespace Game.Models.Decorators
{
    public class PlayerBaseDecorator : Player
    {
        public PlayerBaseDecorator(Player player)
            : base(player.Color, player.Position, player.Control)
        {
            NewPosition = player.NewPosition;
            OldPosition = player.OldPosition;

            GunBulletCount = player.GunBulletCount;
            MiniGunBulletCount = player.MiniGunBulletCount;
            FuelCount = player.FuelCount;
        }

        public Player RollbackPlayer()
        {
            return new Player(Color, Position, Control)
            {
                NewPosition = NewPosition,
                OldPosition = OldPosition,
                FuelCount = FuelCount,
                MiniGunBulletCount = MiniGunBulletCount,
                GunBulletCount = GunBulletCount
            };
        }
    }
}
