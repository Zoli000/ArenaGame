using System;

namespace ArenaGame.Models
{
    public abstract class Fighter
    {
        private static int _nextId = 0;

        protected Fighter(int healthPoints)
        {
            Id = _nextId++;
            MaximumHealthPoints = HealthPoints = healthPoints;
        }

        public int Id { get; init; }

        public bool Survived { get; protected set; } = true;

        public int HealthPoints { get; protected set; }

        private int MaximumHealthPoints { get; init; }

        public void Rest() => HealthPoints = Math.Min(HealthPoints + 10, MaximumHealthPoints);

        public void Die() => Survived = false;

        public (int HealthBefore, int HealthAfter) Fight()
        {
            (int HealthBefore, int HealthAfter) healthStatus = (HealthPoints, HealthPoints /= 2);
            if (healthStatus.HealthAfter < (MaximumHealthPoints / 4))
            {
                Survived = false;
            };

            return healthStatus;
        }

        public abstract FightResult FightAgainst(Fighter opponent);
    }
}
