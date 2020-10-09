using Game.Services;

namespace Game.Factory
{
    /// <summary>
    /// Класс генерирует сервисы коллизий, рисования, действий игрока, действий игры
    /// </summary>
    public static class ServiceFactory
    {
        // сервис коллизий
        public static CollisionService CreateCollisionService()
        {
            return new CollisionService(ComponentsFactory.CreateGameRepository());
        }

        // сервис отрисовки
        public static DrawingService CreateDrawingService()
        {
            return new DrawingService();
        }

        // сервис действий игрока
        public static PlayerActionService CreatePlayerActionService()
        {
            return new PlayerActionService();
        }

        // сервис действий игры
        public static GameActionService CreateGameActionService()
        {
            return new GameActionService(
                CreatePlayerActionService(),
                ComponentsFactory.CreateGameRepository(),
                CreateCollisionService());
        }
    }
}
