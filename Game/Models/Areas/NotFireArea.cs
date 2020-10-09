using Game.Models.Base;
using System.Numerics;

namespace Game.Models.Areas
{
    /// <summary>
    /// Класс территории, на которой невозможны выстрелы
    /// </summary>
    public class NotFireArea : GameAreaObject
    {
        public NotFireArea(Vector2 position)
            : base(position, new Vector4(0, 1, 1, 1))
        {
        }
    }
}
