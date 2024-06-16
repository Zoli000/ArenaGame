namespace ArenaGame.Models
{
    public interface IArenaGameRepository
    {
        public int AddArena(Arena arena);

        public Arena? GetArena(int arenaId);
    }
}
