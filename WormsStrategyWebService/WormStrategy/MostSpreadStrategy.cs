using System;
using System.Collections.Generic;
using WormsStrategyWebService.Models;

namespace WormsStrategyWebService.WormStrategy
{
    public class MostSpreadStrategy: IWormStrategy
    {
        public WormGameStep NextStep(Worm worm, World world, int gameState)
        {
            if (worm.Health > 15)
            {
                return new WormGameStep(WormStep.Spawn, Direction.Right);
            }
            
            var (x, y) = NearestFoodPosition(worm, world.Foods);
            if (worm.Position.X < x) return new WormGameStep(WormStep.Move, Direction.Right);
            if (worm.Position.X > x) return new WormGameStep(WormStep.Move, Direction.Left);
            if (worm.Position.Y > y) return new WormGameStep(WormStep.Move, Direction.Down);
            if (worm.Position.Y < y) return new WormGameStep(WormStep.Move, Direction.Up);

            return new WormGameStep(WormStep.Nothing, Direction.Nothing);
        }

        private (int, int) NearestFoodPosition(Worm worm, List<Food> foods)
        {
            int x = 0, y = 0;
            var minDistance = int.MaxValue;
            
            foreach (var food in foods)
            {
                var distance = Math.Abs(food.Position.X - worm.Position.X) +
                               Math.Abs(food.Position.Y - worm.Position.Y);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    x = food.Position.X;
                    y = food.Position.Y;
                }
            }

            return (x, y);
        }
    }
}