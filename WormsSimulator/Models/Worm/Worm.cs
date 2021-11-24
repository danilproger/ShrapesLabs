using System;
using System.Text.Json.Serialization;
using CS_lab.WormStrategy;

namespace CS_lab.Models
{
    public class Worm
    {
        public string Name { get; }
        public int Health { get; private set; }
        public Position Position { get; private set; }
        [JsonIgnore]
        private readonly IWormStrategy _strategy;

        public Worm(string name, Position position, IWormStrategy strategy)
        {
            Health = 10;
            Name = name;
            Position = position;
            _strategy = strategy;
        }

        public WormGameStep NextStep(World world, int gameStep)
        {
            return _strategy.NextStep(this, world, gameStep);
        }

        public void DecreaseHealth()
        {
            Health--;
        }

        public void IncrementHealth(int healthPoints)
        {
            Health += healthPoints;
        }

        public bool EnableToSpawn()
        {
            return Health > 10;
        }

        public void Move(World world, Position newPosition)
        {
            if (!world.IsWormOnPosition(newPosition))
            {
                Position = newPosition;
            }
        }

        public void Spawn(World world, string nextName, Position newPosition, IWormStrategy wormStrategy)
        {
            if (world.IsWormOnPosition(newPosition) || world.IsFoodOnPosition(newPosition) || !EnableToSpawn())
            {
                return;
            }
            
            var newWorm = new Worm(nextName, newPosition, wormStrategy);
            world.AddWorm(newWorm);
        }
    }
}