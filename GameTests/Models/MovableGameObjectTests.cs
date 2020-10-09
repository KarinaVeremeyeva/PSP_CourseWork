using Game.Models;
using Game.Models.ModelComponents;
using NUnit.Framework;
using System.Numerics;
using System.Windows.Forms;

namespace GameTests.Models
{
    public class MovableGameObjectTests
    {
        [Test]
        public void MoveGameOnject()
        {
            var player = new Player(
               new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
               new Vector2(2, 0.5f),
               new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));
            player.Move();
            Assert.AreEqual(player.Position, player.NewPosition);
        }

        [Test]
        public void RollBackNewCoords_ReturnsPosition()
        {
            var player = new Player(
                new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector2(2, 0.5f),
                new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            player.RollBackNewCoords();
            Assert.AreEqual(player.Position, player.NewPosition);
        }
    }
}
