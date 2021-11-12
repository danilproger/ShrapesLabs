using WorldBehavior.Models;
using WorldBehavior.Utils;

namespace WorldBehavior.FoodGenerator
{
    public class NormallyDistributedFoodGenerator : IFoodGenerator
    {
        public FoodPosition GenerateFood(World world, int gameStep)
        {
            int x, y;

            do
            {
                (x, y) = NormalRandomizer.NextNormalXY();
            } while (world.IsFoodOnPosition(x, y));

            var foodPosition = new FoodPosition() {GameStep = gameStep, X = x, Y = y};
            
            return foodPosition;
        }
    }
}