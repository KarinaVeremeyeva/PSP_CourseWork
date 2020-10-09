using System.Numerics;

namespace Game.Interfaces
{
    public interface IMoveable
    {
        float Speed { get; set; }

        Vector2 OldPosition { get; set; }

        Vector2 NewPosition { get; set; }

        void UpdateNewCoords(float x, float y);

        void Move();

        void RollBackNewCoords();
    }
}
