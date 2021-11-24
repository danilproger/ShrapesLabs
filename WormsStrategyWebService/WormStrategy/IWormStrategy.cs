using WormsStrategyWebService.Models;

namespace WormsStrategyWebService.WormStrategy
{
    public interface IWormStrategy
    {
        public WormGameStep NextStep(Worm worm, World world, int gameState);
    }
}