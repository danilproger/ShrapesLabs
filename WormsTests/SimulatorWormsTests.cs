using System.Collections.Generic;
using CS_lab.Models;
using CS_lab.WormStrategy;
using NUnit.Framework;

namespace WormsTests
{
    [TestFixture]
    public class SimulatorWormsTests
    {
        private World _world;
        private Worm _worm;
        private IWormStrategy _wormStrategy;
        
        [SetUp]
        public void SetUp()
        {
            _wormStrategy = new AroundZeroStrategy();
            _worm = new Worm("1", new Position(0, 0), _wormStrategy);
            
            var wormsList = new List<Worm> {_worm};
            var foodList = new List<Food>();
            _world = new World(wormsList, foodList);
        }
        
        
        [Test]
        public void MoveEmptyCell()
        {
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);

            var oldPosition = _worm.Position;
            var newPosition = _worm.Position.DirectedPosition(nextDirection);

            _worm.Move(_world, newPosition);
            
            Assert.AreNotEqual(oldPosition, _worm.Position);
        }

        [Test]
        public void MoveBusyCell()
        {
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);

            var oldPosition = _worm.Position;
            var newPosition = _worm.Position.DirectedPosition(nextDirection);

            var busyWorm = new Worm("2", newPosition, _wormStrategy);
            _world.AddWorm(busyWorm);

            _worm.Move(_world, newPosition);
            
            Assert.AreEqual(oldPosition, _worm.Position);
        }

        [Test]
        public void MoveFoodCell()
        {
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);

            var oldPosition = _worm.Position;
            var newPosition = _worm.Position.DirectedPosition(nextDirection);

            var food = new Food(newPosition);
            _world.AddFood(food);

            var oldWormHealth = _worm.Health;

            _worm.Move(_world, newPosition);
            
            _world.CheckWormsFoods();
            
            Assert.AreNotEqual(oldPosition, _worm.Position);
            Assert.AreEqual(_worm.Position, food.Position);
            Assert.IsTrue(oldWormHealth < _worm.Health);
        }

        [Test]
        public void SuccessfulSpawn()
        {
            var oldWormsCount = _world.Worms.Count;
            
            _worm.IncrementHealth(1);
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);
            var newPosition = _worm.Position.DirectedPosition(nextDirection);
            
            _worm.Spawn(_world, "2", newPosition, _wormStrategy);
            
            Assert.IsTrue(_world.Worms.Count > oldWormsCount);
        }

        
        [Test]
        public void UnsuccessfulSpawnNotEnoughHealth()
        {
            var oldWormsCount = _world.Worms.Count;
            
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);
            var newPosition = _worm.Position.DirectedPosition(nextDirection);
            
            _worm.Spawn(_world, "2", newPosition, _wormStrategy);
            
            Assert.AreEqual(oldWormsCount, _world.Worms.Count);
        }
        
        [Test]
        public void UnsuccessfulSpawnNotBusyCell()
        {
            var oldWormsCount = _world.Worms.Count;
            
            _worm.IncrementHealth(1);
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);
            var newPosition = _worm.Position.DirectedPosition(nextDirection);

            var food = new Food(newPosition);
            _world.AddFood(food);
            
            _worm.Spawn(_world, "2", newPosition, _wormStrategy);
            
            Assert.AreEqual(oldWormsCount, _world.Worms.Count);
        }
    }
}