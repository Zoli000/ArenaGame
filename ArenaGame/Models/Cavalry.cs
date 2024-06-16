using System;

namespace ArenaGame.Models
{
    public class Cavalry : Fighter
    {
        public Cavalry() : base(150)
        {
        }

        public override FightResult FightAgainst(Fighter opponent)
        {
            return opponent switch
            {
                Cavalry => FightResult.Win,
                Swordsman => FightResult.Lose,
                Archer => FightResult.Win,
                _ => throw new InvalidOperationException("Unknown opponent type")
            };
        }
    }
}
