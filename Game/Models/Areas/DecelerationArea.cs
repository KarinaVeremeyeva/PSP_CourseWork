using Game.Models.Base;
using System.Numerics;

namespace Game.Models.Areas
{
    /// <summary>
    /// Класс территории, на которой происходит замедление
    /// </summary>
    public class DecelerationArea : GameAreaObject
    {
        public DecelerationArea(Vector2 position)
            : base(position, new Vector4(1, 0, 1, 1))
        {
        }
    }
}
