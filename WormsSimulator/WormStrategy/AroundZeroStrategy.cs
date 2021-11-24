using System.Collections.Generic;
using CS_lab.Models;

namespace CS_lab.WormStrategy
{
    public class AroundZeroStrategy : IWormStrategy
    {
        private readonly List<Direction> _pathAroundZero;
        private int _steps;
        
        public AroundZeroStrategy()
        {
            _steps = 0;
            _pathAroundZero = new List<Direction>
            {
                Direction.Down,
                Direction.Left,
                Direction.Left,
                Direction.Up,
                Direction.Up,
                Direction.Right,
                Direction.Right,
                Direction.Down
            };
        }

        public WormGameStep NextStep(Worm worm, World world, int gameState)
        {
            if (gameState == 0)
            {
                return new WormGameStep(WormStep.Move, Direction.Right);
            }

            return new WormGameStep(WormStep.Move, _pathAroundZero[_steps++ % _pathAroundZero.Count]);
        }
    }
}