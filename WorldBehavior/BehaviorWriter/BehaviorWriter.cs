using WorldBehavior.FoodGenerator;
using WorldBehavior.Models;

namespace WorldBehavior.BehaviorWriter
{
    public class BehaviorWriter : IBehaviorWriter
    {
        public void Write(string behaviorName, ApplicationContext dbContext, IFoodGenerator foodGenerator)
        {
            var behavior = new Behavior() {Name = behaviorName};
            var world = new World();
            
            
            for (int i = 0; i < 100; ++i)
            {
                world.DecrementFood();

                var newFood = foodGenerator.GenerateFood(world, i);
                world.AddFood(newFood);
            }

            dbContext.Behavior.Add(behavior);
            dbContext.SaveChanges();

            var foodPositions = world.GetFoodPositions();
            foreach (var foodPosition in foodPositions)
            {
                foodPosition.BehaviorId = behavior.Id;
                dbContext.FoodPositions.Add(foodPosition);
                dbContext.SaveChanges();
            }
        }
    }
}