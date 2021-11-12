using WorldBehavior.FoodGenerator;

namespace WorldBehavior.BehaviorWriter
{
    public interface IBehaviorWriter
    {
        void Write(string behaviorName, ApplicationContext dbContext, IFoodGenerator foodGenerator);
    }
}