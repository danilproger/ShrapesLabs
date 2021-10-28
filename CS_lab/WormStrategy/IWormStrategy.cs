using CS_lab.Models;

namespace CS_lab.WormStrategy
{
    public interface IWormStrategy
    {
        public (WormStep, Direction) NextStep(Worm worm, World world, int gameState);
    }
}