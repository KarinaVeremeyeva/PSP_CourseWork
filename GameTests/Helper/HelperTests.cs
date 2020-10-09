using NUnit.Framework;
using GameHelper = Game.Helpers.Helper;
using Game.Models;
using Game.Models.ModelComponents;
using System.Windows.Forms;
using System.Numerics;

namespace GameTests.Helper
{
    public class HelperTests
    {
        [Test]
        public void GetObjectDirections_DirectionIsRight()
        {
            var player = new Player(
                new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector2(2, 0.5f),
                new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            var (x, y) = GameHelper.GetObjectDirections(player);

            Assert.AreEqual(1, x);
            Assert.AreEqual(0, y);
        }
    }
}
