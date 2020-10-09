using Game.Models;
using Game.Models.Base;
using System.Collections.Generic;

namespace Game
{
    public class GameRepository
    {
        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public List<Wall> Walls { get; set; } = new List<Wall>();

        public List<GunBullet> Bullets { get; set; } = new List<GunBullet>();

        public List<GameAreaObject> GameAreas { get; set; } = new List<GameAreaObject>();

        public List<GamePhysicalObject> GetAllGamePhysicalObjects(GamePhysicalObject excludeObject = null)
        {
            var result = new List<GamePhysicalObject>() { Player1, Player2 };
            result.AddRange(Walls);

            if (excludeObject != null)
            {
                result.Remove(excludeObject);
            }

            return result;
        }

        public List<GameObject> GetAllGameObjects(GamePhysicalObject excludeObject = null)
        {
            var result = new List<GameObject>(GameAreas) { Player1, Player2 };
            result.AddRange(Bullets);
            result.AddRange(Walls);

            if (excludeObject != null)
            {
                result.Remove(excludeObject);
            }

            return result;
        }
    }
}
