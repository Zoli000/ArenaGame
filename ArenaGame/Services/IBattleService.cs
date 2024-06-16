using ArenaGame.DTOs;
using System.Collections.Generic;

namespace ArenaGame.Services
{
    public interface IBattleService
    {
        /// <summary>
        /// Enumerate the turn-based battle results
        /// </summary>
        /// <param name="arenaId"></param>
        /// <returns>Battle log</returns>
        public IEnumerable<BattleRoundResultDto> DoBattle(int arenaId);
    }
}
