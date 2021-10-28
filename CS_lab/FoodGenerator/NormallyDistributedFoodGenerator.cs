using CS_lab.Models;
using CS_lab.Utils;

namespace CS_lab.FoodGenerator
{
    public class NormallyDistributedFoodGenerator : IFoodGenerator
    {
        public Food GenerateFood(World world)
        {
            int x, y;

            do
            {
                (x, y) = NormalRandomizer.NextNormalXY();
            } while (world.IsFoodOnPosition(x, y));

            var foodPosition = new Position(x, y);
            var food = new Food(foodPosition);

            return food;
        }
    }
}