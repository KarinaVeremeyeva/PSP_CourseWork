using Game.Models;
using Game.Models.Decorators;
using Game.Models.ModelComponents;
using NUnit.Framework;
using System.Numerics;
using System.Windows.Forms;

namespace GameTests.Decorators
{
    public class DecelaratingDecoratorTests
    {
        [Test]
        public void DecelaratingDecorator_MoveSlowly()
        {
            var player = new Player(
              new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
              new Vector2(2, 0.5f),
              new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));
            player = new DeceleratingDecorator(player);
            player.UpdateNewCoords(1, 0);

            Assert.AreEqual(2.5f, player.NewPosition.X);
            Assert.AreEqual(0.5f, player.NewPosition.Y);
        }
    }
}
