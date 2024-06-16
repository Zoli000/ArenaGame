using System;

namespace ArenaGame.Models
{
    public class Archer : Fighter
    {
        public Archer() : base(100)
        {
        }

        public override FightResult FightAgainst(Fighter opponent)
        {
            if (opponent is Cavalry)
            {
                Random random = new();
                return random.NextDouble() < 0.40 ? FightResult.Win : FightResult.Draw;
            }
            else if (opponent is Swordsman)
            {
                return FightResult.Win;
            }
            else if (opponent is Archer)
            {
                return FightResult.Win;
            }

            throw new InvalidOperationException("Unknown opponent type");
        }
    }
}
