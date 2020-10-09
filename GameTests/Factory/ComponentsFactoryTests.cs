using Game.Factory;
using NUnit.Framework;
using System.Windows.Forms;

namespace GameTests.Factory
{
    public class ComponentsFactoryTests
    {
        [Test]
        public void CreateControlSettings_SettingsUnchanged()
        {
            var testKey = Keys.A;
            
            var comtrolSettings = ComponentsFactory.CreateControlSettings(
                testKey, testKey, testKey,
                testKey, testKey, testKey);

            Assert.AreEqual(testKey, comtrolSettings.Down);
            Assert.AreEqual(testKey, comtrolSettings.Fire);
            Assert.AreEqual(testKey, comtrolSettings.FireMiniGun);
            Assert.AreEqual(testKey, comtrolSettings.Left);
            Assert.AreEqual(testKey, comtrolSettings.Right);
            Assert.AreEqual(testKey, comtrolSettings.Up);
        }

        [Test]
        public void CreateGameRepository_AlwaysCreatedTheSameRepository()
        {
            var repositoryObject = ComponentsFactory.CreateGameRepository();

            Assert.AreEqual(
                repositoryObject,
                ComponentsFactory.CreateGameRepository());
            Assert.AreEqual(
                repositoryObject,
                ComponentsFactory.CreateGameRepository());
        }
    }
}
