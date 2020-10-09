using Game.Factory;
using Game.Models.ModelComponents;
using NUnit.Framework;
using System.Numerics;
using System.Windows.Forms;

namespace GameTests.Factory
{
    public class GameObjectFactoryTest
    {
        [Test]
        public void CreatePlayer_CompareParameters()
        {
            var position = new Vector2(2, 0.5f);
            var player = GameObjectFactory.CreatePlayer(
               new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
               position,
               new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            Assert.AreEqual(position, player.Position);
            Assert.AreEqual(position, player.NewPosition);
            Assert.AreEqual(position, player.OldPosition);

            var color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
            var player2 = GameObjectFactory.CreatePlayer(
               color,
               new Vector2(2, 0.5f),
               new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            Assert.AreEqual(color, player2.Color);
        }

        [Test]
        public void CreateWall_CompareParameters()
        {
            var position = new Vector2(2, 0.5f);
            var wall = GameObjectFactory.CreateWall(position, 3);

            Assert.AreEqual(position, wall.Position);

            var size = 2;
            var wall2 = GameObjectFactory.CreateWall(new Vector2(2, 0.5f), size);

            Assert.AreEqual(size, wall2.Size);
        }

        [Test]
        public void CreateGunBullet_CompareParameters()
        {
            var player = GameObjectFactory.CreatePlayer(
              new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
              new Vector2(2, 0.5f),
              new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            var gunBullet = GameObjectFactory.CreateGunBullet(player);

            Assert.AreEqual(new Vector2(2.25f, 0.375f), gunBullet.Position);
        }

        [Test]
        public void CreateDecelerationArea_InputParametersAreSame()
        {
            var player = GameObjectFactory.CreatePlayer(
              new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
              new Vector2(2, 0.5f),
              new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            var gunBullet = GameObjectFactory.CreateGunBullet(player);

            Assert.AreEqual(new Vector2(2.25f, 0.375f), gunBullet.Position);
        }

        [Test]
        public void CreateInpassableArea_InputParametersAreSame()
        {
            var position = new Vector2(2, 0.5f);
            var inpassableArea = GameObjectFactory.CreateInpassableArea(position);
            Assert.AreEqual(position, inpassableArea.Position);
        }

        [Test]
        public void CreateNotFireArea_InputParametersAreSame()
        {
            var position = new Vector2(2, 0.5f);
            var notFireArea = GameObjectFactory.CreateNotFireArea(position);
            Assert.AreEqual(position, notFireArea.Position);
        }

        [Test]
        public void CreateDeceleratingDecoratot_InputParametersAreSame()
        {
            var player = GameObjectFactory.CreatePlayer(
             new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
             new Vector2(2, 0.5f),
             new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            var deceleratingDecorator = GameObjectFactory.CreateDeceleratingDecorator(player);
            Assert.AreEqual(player.Position, deceleratingDecorator.Position);
        }

        [Test]
        public void CreateNotFireArea_InputPlayerParameters()
        {
            var player = GameObjectFactory.CreatePlayer(
             new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
             new Vector2(2, 0.5f),
             new ControlSettings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C));

            var notFireArea = GameObjectFactory.CreateNotFireArea(player);
            Assert.AreEqual(player.Position, notFireArea.Position);
            Assert.AreEqual(player.Color, notFireArea.Color);
        }

    }
}
