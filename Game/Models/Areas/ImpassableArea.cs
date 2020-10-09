using Game.Models.Base;
using System.Numerics;

namespace Game.Models.Areas
{
    /// <summary>
    /// Класс для создания территорий, через которые нельзя пройти
    /// </summary>
    public class ImpassableArea : GameAreaObject
    {
        public ImpassableArea(Vector2 position)
            : base(position, new Vector4(1, 1, 1, 1))
        {
        }
    }
}
