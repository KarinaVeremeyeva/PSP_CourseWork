namespace Game
{
    /// <summary>
    /// класс, хранящий константы
    /// </summary>
    public static class Constants
    {
        public const float PlayerSize = 0.25f;
        public const float PlayerSpeed = 0.125f;
        public const int PlayerHealth = 10;
        public const int PlayerGunBulletCount = 100;
        public const int PlayerMiniGunBulletCount = 3;
        public const int PlayerFuelCount = 100;

        public const int BorderWallHealth = int.MaxValue;
        public const float BorderWallSize = 0.2f;
        public const float BorderWallLeftTopX = 5.0f;
        public const float BorderWallLeftTopY = 3.0f;
        public const float BorderWallRigftBottomX = -4.8f;
        public const float BorderWallRigftBottomY = -2.8f;

        public const int WallHealth = 5;
        public const float WallSize = 0.5f;

        public const float GunBulletSize = 0.05f;
        public const float GunBulletSpeed = 0.3f;

        public const float MiniGunBulletSize = 0.1f;

        public const int BulletHealth = 1;

        public const int AreaSize = 1;

        public const float SpeedDivider = 2;

        public const float MaxMapPosition = 100;
    }
}
