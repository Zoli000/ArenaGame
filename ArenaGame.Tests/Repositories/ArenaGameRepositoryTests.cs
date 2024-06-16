using ArenaGame.Models;

namespace ArenaGame.Tests.Repositories
{
    [TestFixture]
    public class ArenaGameRepositoryTests
    {
        private IArenaGameRepository _repository;

        [SetUp]
        public void Setup() => _repository = new ArenaGameRepository();

        [Test]
        public void AddArena_ShouldAssignIdAndAddToList()
        {
            Arena arena = new();

            int id = _repository.AddArena(arena);
            Arena? addedArena = _repository.GetArena(id);

            Assert.Multiple(() =>
            {
                Assert.That(id, Is.EqualTo(0));
                Assert.That(addedArena, Is.Not.Null);
                Assert.That(addedArena?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        public void GetArena_ShouldReturnCorrectArena()
        {
            Arena arena1 = new();
            Arena arena2 = new();
            int arena1id = _repository.AddArena(arena1);
            int arena2id = _repository.AddArena(arena2);

            Arena? retrievedArena1 = _repository.GetArena(arena1id);
            Arena? retrievedArena2 = _repository.GetArena(arena2id);

            Assert.Multiple(() =>
            {
                Assert.That(retrievedArena1, Is.Not.Null);
                Assert.That(retrievedArena1?.Id, Is.EqualTo(arena1id));
                Assert.That(retrievedArena2, Is.Not.Null);
                Assert.That(retrievedArena2?.Id, Is.EqualTo(arena2id));
            });
        }

        [Test]
        public void GetArena_ShouldReturnNullIfNotFound()
        {
            Arena? retrievedArena = _repository.GetArena(999);

            Assert.That(retrievedArena, Is.Null);
        }
    }
}