using System;
using System.Collections.Generic;
using System.Linq;
using CS_lab.BehaviorDatabase;
using CS_lab.BehaviorDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BehaviorTests
{
    public class BehaviorReaderTests
    {
        private readonly DbContextOptions<ApplicationContext> _dbContextOptions;

        public BehaviorReaderTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "bd")
                .Options;
        }
        
        
        [Fact]
        public void ReadEmptyBehavior()
        {
            var behaviorName = "test_name_1";
            using var appContext = new ApplicationContext(_dbContextOptions);
            Assert.Empty(appContext.GetFoodListByBehavior(behaviorName));
        }

        [Fact]
        public void ReadBehavior()
        {
            using var appContext = new ApplicationContext(_dbContextOptions);
            
            var behaviorName = "test_name_2";
            var behavior = new Behavior() {Name = behaviorName};

            appContext.Behavior.Add(behavior);
            appContext.SaveChanges();

            var foodPositions = new List<FoodPosition>();
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                foodPositions.Add(new FoodPosition() {BehaviorId = behavior.Id, GameStep = i, X = random.Next(-5, 5), Y = random.Next(-5, 5)});
            }

            appContext.FoodPositions.AddRange(foodPositions);
            appContext.SaveChanges();
            
            
            
            Assert.Equal(100, appContext.GetFoodListByBehavior(behaviorName).Count);
        }
    }
}