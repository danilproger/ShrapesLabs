using CS_lab.Models;

namespace CS_lab.WormStrategy
{
    public interface IWormStrategy
    {
        public WormGameStep NextStep(Worm worm, World world, int gameState);
    }
}