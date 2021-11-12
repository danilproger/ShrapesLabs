using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorldBehavior;
using WorldBehavior.BehaviorWriter;
using WorldBehavior.FoodGenerator;
using Xunit;

namespace BehaviorTests
{
    public class BehaviorWriterTests
    {
        private readonly DbContextOptions<ApplicationContext> _dbContextOptions;

        public BehaviorWriterTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "BehaviorWriterTests")
                .Options;
        }
        
        
        [Fact]
        public void WriteBehavior()
        {
            var behaviorName = "test_name_1";
            var foodGenerator = new NormallyDistributedFoodGenerator();
            using var appContext = new ApplicationContext(_dbContextOptions);

            Assert.Equal(0, appContext.Behavior.Count());
            Assert.Equal(0, appContext.FoodPositions.Count());

            var writer = new BehaviorWriter();
            writer.Write(behaviorName, appContext, foodGenerator);
            
            Assert.Equal(1, appContext.Behavior.Count());
            Assert.Equal(100, appContext.FoodPositions.Count());
        }
    }
}