namespace ArenaGame.Services
{
    public interface IArenaService
    {
        /// <summary>
        /// Generate new Arena with the given amount or random Fighters
        /// </summary>
        /// <param name="numberOfFighters"></param>
        /// <returns>Generated Arena identifier</returns>
        public int GenerateArena(int numberOfFighters);
    }
}
