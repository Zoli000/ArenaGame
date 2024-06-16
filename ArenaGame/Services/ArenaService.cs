using System;
using ArenaGame.Models;

namespace ArenaGame.Services
{
    public class ArenaService : IArenaService
    {
        private readonly IArenaGameRepository _arenaGameRepository;

        private readonly Random _random = new();

        public ArenaService(IArenaGameRepository arenaRepository)
        {
            this._arenaGameRepository = arenaRepository;
        }

        public int GenerateArena(int numberOfFighters)
        {
            if (numberOfFighters < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfFighters), " should be at least 2");
            }

            Arena arena = new();

            for (int i = 0; i < numberOfFighters; i++)
            {
                Fighter newFighter = _random.Next(3) switch
                {
                    0 => new Archer(),
                    1 => new Swordsman(),
                    2 => new Cavalry(),
                    _ => throw new ArgumentException("Invalid fighter type"),
                };

                arena.AddFighter(newFighter);
            }

            return _arenaGameRepository.AddArena(arena);
        }
    }
}
