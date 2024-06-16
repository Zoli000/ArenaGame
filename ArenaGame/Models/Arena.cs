using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGame.Models
{
    public class Arena
    {
        public int Id { get; set; }

        private readonly List<Fighter> Fighters = new();

        public int FighterCount => Fighters.Count;

        public void AddFighter(Fighter newFighter) => Fighters.Add(newFighter);

        public Fighter? GetFirstFighter() => Fighters.FirstOrDefault();

        public Fighter? GetFighter(int id) => Fighters[id];

        public IEnumerable<Fighter> GetAllFighters() => Fighters;

        public IEnumerable<Fighter> GetAllFighters(Func<Fighter, bool> predicate) => Fighters.Where(predicate);

        public void RemoveFighter(Fighter deadFighter) => Fighters.Remove(deadFighter);
    }
}
