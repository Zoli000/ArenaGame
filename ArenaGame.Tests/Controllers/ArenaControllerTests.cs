using ArenaGame.Controllers;
using ArenaGame.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ArenaGame.Tests.Controllers
{
    [TestFixture]
    public class ArenaControllerTests
    {
        private Mock<IArenaService> _arenaServiceMock;
        private ArenaController _arenaController;

        [SetUp]
        public void Setup()
        {
            _arenaServiceMock = new Mock<IArenaService>();
            _arenaController = new ArenaController(_arenaServiceMock.Object);
        }

        [Test]
        public void Index_ShouldReturnOkResult_WithArenaId()
        {
            int numberOfFighters = 10;
            int arenaId = 0;
            _ = _arenaServiceMock.Setup(service => service.GenerateArena(numberOfFighters)).Returns(arenaId);

            IActionResult result = _arenaController.GenerateArena(numberOfFighters);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<OkObjectResult>());
                Assert.That((result as OkObjectResult)?.Value, Is.EqualTo(arenaId));
            });
        }

        [Test]
        public void Index_ShouldReturnsBadRequestResultIfArgumentInvalid()
        {
            int numberOfFighters = -10;

            IActionResult result = _arenaController.GenerateArena(numberOfFighters);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}