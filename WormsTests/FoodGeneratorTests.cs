using System.Collections.Generic;
using CS_lab.FoodGenerator;
using CS_lab.Models;
using NUnit.Framework;

namespace WormsTests
{
    public class FoodGeneratorTests
    {
        private IFoodGenerator _foodGenerator;
        private World _world;

        [SetUp]
        public void SetUp()
        {
            _foodGenerator = new NormallyDistributedFoodGenerator();
            var wormsList = new List<Worm>();
            var foodList = new List<Food>();
            _world = new World(wormsList, foodList);
        }

        [Test]
        public void UniqueFoods()
        {
            for (int i = 0; i < 10; ++i)
            {
                var food = _foodGenerator.GenerateFood(_world, i);
                _world.AddFood(food);
            }

            var notUniqueFoodsCount = 0;

            foreach (var food in _world.Foods)
            {
                foreach (var otherFood in _world.Foods)
                {
                    if (food.Position.X == otherFood.Position.X && food.Position.Y == otherFood.Position.Y)
                    {
                        notUniqueFoodsCount++;
                    }
                }
            }
            
            Assert.AreEqual(notUniqueFoodsCount, _world.Foods.Count);
        }
    }
}