using Moq;
using ArenaGame.Models;
using ArenaGame.Services;
using System.Linq;
using System;

namespace ArenaGame.Tests.Services
{
    [TestFixture]
    public class ArenaServiceTests
    {
        private Mock<IArenaGameRepository> _arenaGameRepositoryMock;
        private ArenaService _arenaService;

        [SetUp]
        public void Setup()
        {
            _arenaGameRepositoryMock = new Mock<IArenaGameRepository>();
            _arenaService = new ArenaService(_arenaGameRepositoryMock.Object);
        }

        [Test]
        public void GenerateArena_ShouldCreateCorrectNumberOfFighters()
        {
            int numberOfFighters = 10;
            int expectedArenaId = 42;
            _ = _arenaGameRepositoryMock.Setup(repo => repo.AddArena(It.IsAny<Arena>())).Returns(expectedArenaId);

            int arenaId = _arenaService.GenerateArena(numberOfFighters);

            Assert.That(arenaId, Is.EqualTo(expectedArenaId));
            _arenaGameRepositoryMock.Verify(repo => repo.AddArena(It.Is<Arena>(arena => arena.FighterCount == numberOfFighters)), Times.Once);
        }

        [Test]
        public void GenerateArena_ShouldCreateCorrectFighterIds()
        {
            int numberOfFighters = 6;
            int[] expectedFighterIds = Enumerable.Range(0, numberOfFighters).ToArray();

            int arenaId = _arenaService.GenerateArena(numberOfFighters);

            _arenaGameRepositoryMock.Verify(repo => repo.AddArena(It.Is<Arena>(arena =>
               arena.GetAllFighters().Select(fighter => fighter.Id).SequenceEqual(expectedFighterIds)
            )), Times.Once);
        }

        [Test]
        public void GenerateArena_ShouldThrowExceptionIfArgumentInvalid()
        {
            int numberOfFighters = -10;

            _ = Assert.Throws<ArgumentOutOfRangeException>(() => _arenaService.GenerateArena(numberOfFighters));
            _arenaGameRepositoryMock.Verify(repo => repo.AddArena(It.IsAny<Arena>()), Times.Never);
        }
    }
}
