using CS_lab.Models;

namespace CS_lab.FoodGenerator
{
    public interface IFoodGenerator
    {
        public Food GenerateFood(World world, int gameStep);
    }
}