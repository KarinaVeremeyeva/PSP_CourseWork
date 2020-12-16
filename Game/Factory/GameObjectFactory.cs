using Game.Models;
using Game.Models.Areas;
using Game.Models.Decorators;
using Game.Models.ModelComponents;
using System.Numerics;

namespace Game.Factory
{
    /// <summary>
    /// Класс генерирует игровые объекты: игроки, стены, снаряды, пули, территории.
    /// </summary>
    public static class GameObjectFactory
    {
        //созадние игрока
        public static Player CreatePlayer(Vector4 color, Vector2 position, ControlSettings controlSettings)
        {
            return new Player(color, position, controlSettings);
        }

        // создние стены
        public static Wall CreateWall(Vector2 position, float size, int armor = Constants.WallHealth)
        {
            return new Wall(position, size, armor);
        }

        // созадние пуль
        public static GunBullet CreateGunBullet(Player player)
        {
            var (directionX, directionY) = Helper.Helper.GetObjectDirections(player);

            return new GunBullet(
                new Vector2(
                    player.Position.X + player.Size * directionX - (directionX == 0 ? player.Size / 2 : 0),
                    player.Position.Y + player.Size * directionY - (directionY == 0 ? player.Size / 2 : 0)),
                directionX,
                directionY);
        }

        // созадние мгновенных снарядов
        public static MiniGunBullet CreateMiniGunBullet(Player player)
        {
            var collisionService = ServiceFactory.CreateCollisionService();

            var (directionX, directionY) = Helper.Helper.GetObjectDirections(player);

            var nearestObject = collisionService.GetNearestObjectInPath(player);

            if (nearestObject == null)
            {
                return null;
            }

            return new MiniGunBullet(
                new Vector2(
                    nearestObject.Position.X,
                    nearestObject.Position.Y),
                directionX,
                directionY);
        }

        // создание территорий
        public static DecelerationArea CreateDecelerationArea(Vector2 position)
        {
            return new DecelerationArea(position);
        }

        public static ImpassableArea CreateInpassableArea(Vector2 position)
        {
            return new ImpassableArea(position);
        }

        public static NotFireArea CreateNotFireArea(Vector2 position)
        {
            return new NotFireArea(position);
        }

        // декоратор, замедляющий игрока
        public static DeceleratingDecorator CreateDeceleratingDecorator(Player player)
        {
            return new DeceleratingDecorator(player);
        }

        // декоратор, запрещающий стрелять игроку
        public static NotFireDecorator CreateNotFireArea(Player player)
        {
            return new NotFireDecorator(player);
        }
    }
}
