using Game.Models;
using System.Windows.Forms;

namespace Game.Services
{
    /// <summary>
    /// Класс действий игрока
    /// </summary>
    public class PlayerActionService
    {
        /// <summary>
        /// Движение игрока
        /// </summary>
        /// <param name="player">игрок</param>
        /// <param name="key">клавиша</param>
        public void ProcessPlayerMoving(Player player, Keys key)
        {
            if (key == player.Control.Up)
            {
                player.UpdateNewCoords(0, player.Speed);
            }
            else if (key == player.Control.Down)
            {
                player.UpdateNewCoords(0, -player.Speed);
            }
            else if (key == player.Control.Left)
            {
                player.UpdateNewCoords(+player.Speed, 0);
            }
            else if (key == player.Control.Right)
            {
                player.UpdateNewCoords(-player.Speed, 0);
            }
        }

        public bool IsPlayerMoved(Player player, Keys key)
        {
            return key == player.Control.Up ||
                key == player.Control.Down ||
                key == player.Control.Left ||
                key == player.Control.Right;
        }

        public bool IsPlayerFired(Player player, Keys key)
        {
            return player.Control.Fire == key;
        }

        public bool IsPlayerFiredByMiniGun(Player player, Keys key)
        {
            return player.Control.FireMiniGun == key;
        }
    }
}