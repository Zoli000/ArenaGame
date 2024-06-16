using ArenaGame.DTOs;
using ArenaGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGame.Services
{
    public class BattleService : IBattleService
    {
        private readonly IArenaGameRepository _arenaGameRepository;

        public BattleService(IArenaGameRepository arenaRepository)
        {
            this._arenaGameRepository = arenaRepository;
        }

        public IEnumerable<BattleRoundResultDto> DoBattle(int arenaId)
        {
            Arena currentArena;
            try
            {
                currentArena = CheckArenaValidity(arenaId);
            }
            catch (Exception)
            {
                throw;
            }

            Random _random = new();
            Fighter? attacker;
            Fighter? defender;
            int currentRoundIndex = 1;

            while (currentArena.FighterCount > 1)
            {
                int roundIndex = currentRoundIndex++;

                attacker = currentArena.GetFighter(_random.Next(currentArena.FighterCount));
                if (attacker == null)
                {
                    yield break;
                }

                List<Fighter> remainingFighters = currentArena.GetAllFighters(x => x != attacker).ToList();
                defender = remainingFighters.ElementAt(_random.Next(remainingFighters.Count - 1));

                foreach (Fighter fighter in currentArena.GetAllFighters(x => x != attacker && x != defender))
                {
                    fighter.Rest();
                }

                var (attackerHealthBefore, attackerHealthAfter) = attacker.Fight();
                var (defenderHealthBefore, defenderHealthAfter) = defender.Fight();

                FightResult fightResult = attacker.FightAgainst(defender);

                switch (fightResult)
                {
                    case FightResult.Win:
                        defender.Die();
                        break;
                    case FightResult.Lose:
                        attacker.Die();
                        break;
                    default:
                        break;
                }

                if (!attacker.Survived)
                {
                    currentArena.RemoveFighter(attacker);
                }

                if (!defender.Survived)
                {
                    currentArena.RemoveFighter(defender);
                }

                yield return new BattleRoundResultDto(
                    roundIndex,
                    new FighterStatusDto(attacker.Id, attacker.GetType().Name, attackerHealthBefore, attackerHealthAfter, attacker.Survived),
                    new FighterStatusDto(defender.Id, defender.GetType().Name, defenderHealthBefore, defenderHealthAfter, defender.Survived),
                    fightResult
                );
            }
        }

        private Arena CheckArenaValidity(int arenaId)
        {
            Arena? arena = _arenaGameRepository.GetArena(arenaId) ?? throw new ArgumentException($"The arena with ID {arenaId} is nowhere to be found!");

            switch (arena.FighterCount)
            {
                case 0:
                    throw new ArgumentException("There is nobody in this arena!");

                case 1:
                    throw new ArgumentException($"No battle can be made, only a lonely {arena.GetFirstFighter()?.GetType().Name} left in the arena!");

                default:
                    break;
            }

            return arena;
        }
    }
}
