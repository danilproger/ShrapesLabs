using System;
using System.Collections.Generic;
using CS_lab.Models;

namespace CS_lab.WormStrategy
{
    public class NearestFoodStrategy : IWormStrategy
    {
        public (WormStep, Direction) NextStep(Worm worm, World world, int gameState)
        {
            var (x, y) = NearestFoodPosition(worm, world.Foods);
            if (worm.Position.X < x) return (WormStep.Move, Direction.Right);
            if (worm.Position.X > x) return (WormStep.Move, Direction.Left);
            if (worm.Position.Y > y) return (WormStep.Move, Direction.Down);
            if (worm.Position.Y < y) return (WormStep.Move, Direction.Up);

            return (WormStep.Nothing, Direction.Nothing);
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