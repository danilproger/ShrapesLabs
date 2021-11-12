using System;
using CS_lab.WormStrategy;

namespace CS_lab.Models
{
    public class Worm
    {
        private readonly string _id;
        private readonly string _name;
        private int _health;
        private Position _position;
        private IWormStrategy _strategy;

        public string Id
        {
            get => _id;
        }

        public int Health
        {
            get => _health;
        }

        public Position Position
        {
            get => _position;
            set => _position = value;
        }

        public string Name
        {
            get => _name;
        }

        public Worm(string name, Position position, IWormStrategy strategy)
        {
            _id = Guid.NewGuid().ToString();
            _health = 10;
            _name = name;
            _position = position;
            _strategy = strategy;
        }

        public (WormStep, Direction) NextStep(World world, int gameStep)
        {
            return _strategy.NextStep(this, world, gameStep);
        }

        public void DecreaseHealth()
        {
            _health--;
        }

        public void IncrementHealth(int healthPoints)
        {
            _health += healthPoints;
        }

        public bool EnableToSpawn()
        {
            return _health > 10;
        }

        public void Move(World world, Position newPosition)
        {
            if (!world.IsWormOnPosition(newPosition))
            {
                _position = newPosition;
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