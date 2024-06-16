using Moq;
using ArenaGame.Models;
using ArenaGame.Services;
using ArenaGame.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ArenaGame.Tests.Services
{
    [TestFixture]
    public class BattleServiceTests
    {
        private Mock<IArenaGameRepository> _arenaGameRepositoryMock;
        private BattleService _battleService;

        [SetUp]
        public void Setup()
        {
            _arenaGameRepositoryMock = new Mock<IArenaGameRepository>();
            _battleService = new BattleService(_arenaGameRepositoryMock.Object);
        }

        [Test]
        public void DoBattle_ShouldCreateBattleLog()
        {
            Arena arena = new();
            arena.AddFighter(new Archer());
            arena.AddFighter(new Swordsman());
            arena.AddFighter(new Cavalry());
            _ = _arenaGameRepositoryMock.Setup(repo => repo.GetArena(It.IsAny<int>())).Returns(arena);

            List<BattleRoundResultDto> result = _battleService.DoBattle(0).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.AtLeast(2));
                Assert.That(result[0].RoundIndex, Is.EqualTo(1));
                Assert.That(result[0].Fighter, Is.InstanceOf<FighterStatusDto>());
            });

            _arenaGameRepositoryMock.Verify(repo => repo.GetArena(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void DoBattle_ShouldThrowArgumentExceptionIfArenaNotFound()
        {
            _ = Assert.Throws<ArgumentException>(() => _battleService.DoBattle(0).ToList());

            _arenaGameRepositoryMock.Verify(repo => repo.GetArena(It.IsAny<int>()), Times.Once);
        }
    }
}
