using System.Windows.Forms;

namespace Game.Models.ModelComponents
{
    public class ControlSettings
    {
        public Keys Up { get; }

        public Keys Down { get; }

        public Keys Left { get; }

        public Keys Right { get; }

        public Keys Fire { get; }

        public Keys FireMiniGun { get; }

        public ControlSettings(Keys up, Keys down, Keys left, Keys right, Keys fire, Keys fireMiniGun)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            Fire = fire;
            FireMiniGun = fireMiniGun;
        }
    }
}
