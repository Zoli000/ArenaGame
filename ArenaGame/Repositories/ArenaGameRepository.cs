using System.Collections.Generic;
using System.Linq;

namespace ArenaGame.Models
{
    public class ArenaGameRepository : IArenaGameRepository
    {
        private readonly List<Arena> _arenaList = new();
        private int _nextArenaId = 0;

        public int AddArena(Arena arena)
        {
            arena.Id = _nextArenaId++;
            _arenaList.Add(arena);
            return arena.Id;
        }

        public Arena? GetArena(int arenaId) => _arenaList.FirstOrDefault(a => a.Id == arenaId);
    }
}
