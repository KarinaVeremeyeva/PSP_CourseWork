using Game.Helpers;
using Game.Models;
using Game.Models.Areas;
using Game.Models.Base;
using Game.Models.Decorators;
using System;
using System.Linq;
using System.Numerics;

namespace Game.Services
{
    /// <summary>
    /// Класс сервиса коллизий
    /// </summary>
    public class CollisionService
    {
        private readonly GameRepository gameRepository;

        public CollisionService(GameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        #region Collision cheching

        public bool IsCollisionPossible(MovableGameObject movableObject1, MovableGameObject movableObject2)
        {
            return IsCollisionPossible(
                movableObject1.NewPosition, movableObject1.Size,
                movableObject2.NewPosition, movableObject2.Size);
        }

        public bool IsCollisionPossible(MovableGameObject movableObject, GameObject gameObject)
        {
            return IsCollisionPossible(
                movableObject.NewPosition, movableObject.Size,
                gameObject.Position, gameObject.Size);
        }

        public bool IsCollisionPossible(MovableGameObject movableObject, NotMovableGameObject notMovableObject)
        {
            return IsCollisionPossible(movableObject, (GameObject)notMovableObject);
        }

        public bool IsCollisionPossible(GunBullet bullet, MovableGameObject movableGameObject)
        {
            return IsCollisionPossible(bullet, movableGameObject.NewPosition) ||
                IsCollisionPossible(bullet, new Vector2(
                    movableGameObject.NewPosition.X - movableGameObject.Size,
                    movableGameObject.NewPosition.Y - movableGameObject.Size)) ||
                IsCollisionPossible((MovableGameObject)bullet, movableGameObject);
        }

        public bool IsCollisionPossible(GunBullet bullet, GamePhysicalObject gamePhysicalObject)
        {
            if (gamePhysicalObject is MovableGameObject)
            {
                return IsCollisionPossible(bullet, (MovableGameObject)gamePhysicalObject);
            }

            return IsCollisionPossible(bullet, (NotMovableGameObject)gamePhysicalObject);
        }

        public bool IsCollisionPossible(GunBullet bullet, NotMovableGameObject notMovableGameObject)
        {
            return IsCollisionPossible(bullet, notMovableGameObject.Position) ||
                IsCollisionPossible(bullet, new Vector2(
                    notMovableGameObject.Position.X - notMovableGameObject.Size,
                    notMovableGameObject.Position.Y - notMovableGameObject.Size)) ||
                IsCollisionPossible((MovableGameObject)bullet, notMovableGameObject);
        }

        private bool IsCollisionPossible(GunBullet bullet, Vector2 point)
        {
            var xPositions = new float[]
            {
                bullet.Position.X,
                bullet.Position.X - bullet.Size,
                bullet.NewPosition.X,
                bullet.NewPosition.X - bullet.Size
            };
            var xMax = xPositions.Max();
            var xMin = xPositions.Min();

            var yPositions = new float[]
            {
                bullet.Position.Y,
                bullet.Position.Y - bullet.Size,
                bullet.NewPosition.Y,
                bullet.NewPosition.Y - bullet.Size
            };
            var yMax = yPositions.Max();
            var yMin = yPositions.Min();

            return point.X <= xMax && point.X >= xMin &&
                point.Y <= yMax && point.Y >= yMin;
        }

        private bool IsCollisionPossible(
            Vector2 firstObjectPoint, float firstObjectSize,
            Vector2 secondObjectPoint, float secondObjectSize = Constants.MiniGunBulletSize)
        {
            return firstObjectPoint.X > secondObjectPoint.X - secondObjectSize &&
                firstObjectPoint.X - firstObjectSize < secondObjectPoint.X &&
                firstObjectPoint.Y > secondObjectPoint.Y - secondObjectSize &&
                firstObjectPoint.Y - firstObjectSize < secondObjectPoint.Y;
        }

        #endregion

        public void ProcessCollision(GunBullet bullet, GamePhysicalObject gamePhisicalObject)
        {
            bullet.GetDamage();
            gamePhisicalObject.GetDamage();
        }

        public Player ProcessCollision(Player player, DecelerationArea _)
        {
            return new DeceleratingDecorator(player);
        }

        public Player ProcessCollision(Player player, NotFireArea _)
        {
            return new NotFireDecorator(player);
        }

        public void ProcessCollision(Player player)
        {
            player.RollBackNewCoords();
        }

        public GamePhysicalObject GetNearestObjectInPath(MovableGameObject movableObject)
        {
            var (directionX, directionY) = Helper.GetObjectDirections(movableObject);

            return GetNearestObjectInPath(movableObject, directionX, directionY);
        }

        public GamePhysicalObject GetNearestObjectInPath(GunBullet bullet)
        {
            return GetNearestObjectInPath(bullet, bullet.DirectionX, bullet.DirectionY);
        }

        private GamePhysicalObject GetNearestObjectInPath(MovableGameObject movableObject, int directionX, int directionY)
        {
            var gameObjectsInLine = gameRepository.GetAllGamePhysicalObjects(movableObject);

            var positionX = movableObject.NewPosition.X;
            var positionY = movableObject.NewPosition.Y;

            if (directionX == 0)
            {
                gameObjectsInLine = gameObjectsInLine
                    .Where(q => q.Position.X >= positionX && positionX >= q.Position.X - q.Size)
                    .Where(q =>
                    {
                        return (directionY > 0
                                ? positionY < q.Position.Y
                                : positionY > q.Position.Y) ||
                            IsCollisionPossible(movableObject, q);
                    })
                    .OrderBy(q => Math.Abs(positionY - q.Position.Y))
                    .ToList();
            }
            else
            {
                gameObjectsInLine = gameObjectsInLine
                    .Where(q => q.Position.Y >= positionY && positionY >= q.Position.Y - q.Size).ToList();
                gameObjectsInLine = gameObjectsInLine
                    .Where(q =>
                    {
                        return (directionX > 0
                                ? positionX < q.Position.X
                                : positionX > q.Position.X) ||
                            IsCollisionPossible(movableObject, q);
                    })
                    .OrderBy(q => Math.Abs(positionX - q.Position.X))
                    .ToList();
            }

            return gameObjectsInLine.FirstOrDefault();
        }
    }
}

