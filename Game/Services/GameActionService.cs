using Game.Models;
using Game.Models.Areas;
using Game.Models.Decorators;
using System.Linq;
using System.Windows.Forms;

namespace Game.Services
{
    /// <summary>
    /// Класс сервиса действий игры
    /// </summary>
    public class GameActionService
    {
        private readonly PlayerActionService playerActionService;
        private readonly GameRepository gameRepository;
        private readonly CollisionService collisionService;

        public GameActionService(PlayerActionService playerActionService, GameRepository gameField, CollisionService collisionService)
        {
            this.playerActionService = playerActionService;
            this.gameRepository = gameField;
            this.collisionService = collisionService;
        }

        public void ProcessPlayerAction(Keys key)
        {
            gameRepository.Player1 = ProcessPlayerAction(gameRepository.Player1, key);
            gameRepository.Player2 = ProcessPlayerAction(gameRepository.Player2, key);
        }

        private Player ProcessPlayerAction(Player player, Keys key)
        {
            var anotherPlayer = gameRepository.Player1 == player
                ? gameRepository.Player2
                : gameRepository.Player1;
            var bullets = gameRepository.Bullets;
            var walls = gameRepository.Walls;
            var areas = gameRepository.GameAreas;

            // Выстрел игрока
            if (playerActionService.IsPlayerFired(player, key))
            {
                var newBullet = player.GunFire();
                if (newBullet != null)
                {
                    bullets.Add(newBullet);
                }
            }
            // Выстрел из пулемёта игрока
            else if (playerActionService.IsPlayerFiredByMiniGun(player, key))
            {
                var newBullet = player.MiniGunFire();
                if (newBullet != null)
                {
                    bullets.Add(newBullet);
                }
            }
            // Движение игрока
            // Проверка на коллизии с другими игроками
            // Проверка на коллизии со стенами
            // Проверка на коллизию с непроходимой территорией
            else if (playerActionService.IsPlayerMoved(player, key))
            {
                playerActionService.ProcessPlayerMoving(player, key);

                // Если нет коллизии - движение
                if (!collisionService.IsCollisionPossible(player, anotherPlayer) &&
                    !walls.Any(q => collisionService.IsCollisionPossible(player, q)) &&
                    !areas.Any(q => q is ImpassableArea && collisionService.IsCollisionPossible(player, q)))
                {
                    // Коллизия с территориями-декораторами
                    var collisionArea = areas.FirstOrDefault(q => collisionService.IsCollisionPossible(player, q));
                    if (collisionArea != null)
                    {
                        switch (collisionArea)
                        {
                            case DecelerationArea decelerationArea:
                                player = collisionService.ProcessCollision(player, decelerationArea);
                                break;
                            case NotFireArea notFireArea:
                                player = collisionService.ProcessCollision(player, notFireArea);
                                break;
                        }
                    }
                    else if (player is PlayerBaseDecorator)
                    {
                        player = (player as PlayerBaseDecorator).RollbackPlayer();
                    }

                    player.Move();
                }
                // Если коллизия - обрабатываем
                else
                {
                    collisionService.ProcessCollision(player);
                }
            }

            return player;
        }

        public void ProcessBulletsCollisions()
        {
            // Проверка всех пуль
            foreach (var bullet in gameRepository.Bullets)
            {
                var nearestObject = collisionService.GetNearestObjectInPath(bullet);
                if (!bullet.IsDeadObject() && nearestObject != null && collisionService.IsCollisionPossible(bullet, nearestObject))
                {
                    collisionService.ProcessCollision(bullet, nearestObject);
                }
            }
        }

        public void ProcessBulletsMoving()
        {
            foreach (var bullet in gameRepository.Bullets)
            {
                if (bullet.IsDeadObject())
                {
                    continue;
                }
                bullet.Move();
            }
        }

        public bool IsFirstPlayerWon()
        {
            return gameRepository.Player2.IsDeadObject();
        }

        public bool IsSecondPlayerWon()
        {
            return gameRepository.Player1.IsDeadObject();
        }

        public void DisposeDeadObjects()
        {
            var deadBullets = gameRepository.Bullets.Where(q => q.IsDeadObject()).ToList();
            var deadWalls = gameRepository.Walls.Where(q => q.IsDeadObject()).ToList();

            foreach (var deadBullet in deadBullets)
            {
                gameRepository.Bullets.Remove(deadBullet);
            }

            foreach (var deadWall in deadWalls)
            {
                gameRepository.Walls.Remove(deadWall);
            }
        }
    }
}
