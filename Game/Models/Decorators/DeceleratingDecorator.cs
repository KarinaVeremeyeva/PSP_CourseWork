namespace Game.Models.Decorators
{
    public class DeceleratingDecorator : PlayerBaseDecorator
    {
        private readonly float speedDivider;

        public DeceleratingDecorator(Player player) 
            : base (player)
        {
            speedDivider = Constants.SpeedDivider;
        }

        public override void UpdateNewCoords(float x, float y)
        {
            base.UpdateNewCoords(x / speedDivider, y / speedDivider);
        }
    }
}
