using System;
using System.Collections.Generic;
using CS_lab.FoodGenerator;
using CS_lab.Models;
using CS_lab.WormStrategy;
using NUnit.Framework;

namespace WormsTests
{
    public class WormStrategyTests
    {
        private World _world;
        private Worm _worm;
        private IFoodGenerator _foodGenerator;
        private IWormStrategy _wormStrategy;
        
        [SetUp]
        public void SetUp()
        {
            _foodGenerator = new NormallyDistributedFoodGenerator();
            _wormStrategy = new NearestFoodStrategy();
            _worm = new Worm("1", new Position(100, 100), _wormStrategy);
            
            var wormsList = new List<Worm> {_worm};
            var foodList = new List<Food>();
            _world = new World(wormsList, foodList);
        }

        [Test]
        public void MoveToNearestFood()
        {
            var food = _foodGenerator.GenerateFood(_world, 0);
            _world.AddFood(food);

            var oldDistance = Math.Abs(_worm.Position.X - food.Position.X) +
                              Math.Abs(_worm.Position.Y - food.Position.Y);
            
            var (_, nextDirection) = _wormStrategy.NextStep(_worm, _world, 0);
            var newPosition = _worm.Position.DirectedPosition(nextDirection);

            _worm.Move(_world, newPosition);
            
            var newDistance = Math.Abs(_worm.Position.X - food.Position.X) +
                              Math.Abs(_worm.Position.Y - food.Position.Y);
            
            Assert.IsTrue(oldDistance > newDistance);
        }
    }
}