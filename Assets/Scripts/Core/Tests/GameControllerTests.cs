using NSubstitute;
using NUnit.Framework;

namespace Core.Tests
{
    public class GameControllerTests
    {
        private GameController controller;

        [SetUp]
        public void Setup()
        {
            var uiObject = Substitute.For<IUiObject>();
            var ga = Substitute.For<IGeneticAlgorithm>();
            var mainGameObject = Substitute.For<IMainGameObject>();

            controller = new GameController(uiObject, ga, mainGameObject);
        }

        [Test]
        public void foo()
        {
            Assert.Pass();
        }
    }
}