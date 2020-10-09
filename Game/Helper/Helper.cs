using Game.Models.Base;
using System;

namespace Game.Helpers
{
    /// <summary>
    /// Класс для определения направления объекта
    /// </summary>
    public static class Helper
    {
        public static (int, int) GetObjectDirections(MovableGameObject movableObject)
        {
            // Если двигались вправо, направление 1
            // Если двигались влево, направление -1
            var directionX = movableObject.Position.X - movableObject.OldPosition.X;
            if (directionX != 0)
            {
                directionX /= Math.Abs(directionX);
            }

            // Если двигались вверх, направление 1
            // Если двигались вниз, направление -1
            var directionY = movableObject.Position.Y - movableObject.OldPosition.Y;
            if (directionY != 0)
            {
                directionY /= Math.Abs(directionY);
            }

            // Если изначально стояли на месте, стреляем вправо
            if (directionX == 0 && directionY == 0)
            {
                directionX = 1;
            }

            return ((int)directionX, (int)directionY);
        }
    }
}
