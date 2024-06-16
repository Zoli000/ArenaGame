using ArenaGame.Controllers;
using ArenaGame.DTOs;
using ArenaGame.Models;
using ArenaGame.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace ArenaGame.Tests.Controllers
{
    [TestFixture]
    public class BattleControllerTests
    {
        private Mock<IBattleService> _battleServiceMock;
        private BattleController _battleController;

        [SetUp]
        public void Setup()
        {
            _battleServiceMock = new Mock<IBattleService>();
            _battleController = new BattleController(_battleServiceMock.Object);
        }

        [Test]
        public void Index_ShouldReturnOkResult_WithBattleLog()
        {
            int arenaId = 0;
            List<BattleRoundResultDto> expectedResult = new()
            {
                new BattleRoundResultDto(
                    0,
                    new FighterStatusDto(0, "Archer", 100, 50, true),
                    new FighterStatusDto(1, "Archer", 100, 50, false),
                    FightResult.Win
                )
            };
            _ = _battleServiceMock.Setup(service => service.DoBattle(arenaId)).Returns(expectedResult);

            IActionResult result = _battleController.Get(arenaId);
            OkObjectResult? viewResult = result as OkObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<OkObjectResult>());
                Assert.That(viewResult?.Value, Is.EqualTo(expectedResult));
            });
        }

        [Test]
        public void Index_ShouldReturnsNotFoundResultIfArgumentInvalid()
        {
            int arenaId = -10;

            IActionResult result = _battleController.Get(arenaId);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}