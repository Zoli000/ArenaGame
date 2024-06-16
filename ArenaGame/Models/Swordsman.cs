using System;

namespace ArenaGame.Models
{
    public class Swordsman : Fighter
    {
        public Swordsman() : base(120)
        {
        }

        public override FightResult FightAgainst(Fighter opponent)
        {
            return opponent switch
            {
                Cavalry => FightResult.Draw,
                Swordsman => FightResult.Win,
                Archer => FightResult.Win,
                _ => throw new InvalidOperationException("Unknown opponent type")
            };
        }
    }
}