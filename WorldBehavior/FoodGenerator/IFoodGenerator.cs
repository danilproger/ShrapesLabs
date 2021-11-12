using WorldBehavior.Models;

namespace WorldBehavior.FoodGenerator
{
    public interface IFoodGenerator
    {
        public FoodPosition GenerateFood(World world, int gameStep);
    }
}