using Game.Models;
using Game.Models.Decorators;
using Game.Models.ModelComponents;
using NUnit.Framework;
using System.Numerics;
using System.Windows.Forms;

namespace GameTests.Decorators
{
    public class NotFireDecoratorTests
    {
        [Test]
        public void NotFireDecorator_ReturnsNull()
        {
            var player = new Player(
              new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
              new Vector2(2, 0.5f),
              new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));
            player = new NotFireDecorator(player);
            player.GunFire();
           
            Assert.AreEqual(null, player.GunFire());
            Assert.AreEqual(null, player.MiniGunFire());
        }
    }
}
