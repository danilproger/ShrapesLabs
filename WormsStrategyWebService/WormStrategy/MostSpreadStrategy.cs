using System;
using System.Collections.Generic;
using WormsStrategyWebService.Models;

namespace WormsStrategyWebService.WormStrategy
{
    public class MostSpreadStrategy: IWormStrategy
    {
        public WormGameStep NextStep(Worm worm, World world, int gameState)
        {
            /*if (worm.Health > 13)
            {
                return new WormGameStep(Direction.Right, true);
            }
            
            var (x, y) = NearestFoodPosition(worm, world.Foods);
            if (worm.Position.X < x) return new WormGameStep(Direction.Right,false);
            if (worm.Position.X > x) return new WormGameStep(Direction.Left, false);
            if (worm.Position.Y > y) return new WormGameStep(Direction.Down, false);
            if (worm.Position.Y < y) return new WormGameStep(Direction.Up, false);

            return new WormGameStep();*/

            if (gameState > 67)
            {
                if (worm.Health > 30 && gameState%2 == 0)
                {
                    var spawnDirection = FindSwapPosition(worm, world);
                    return new WormGameStep(spawnDirection, true);
                }
                else
                {
                    var (x, y) = NearestFoodPosition(worm, world.Foods);
                    if (worm.Position.X < x) return new WormGameStep(Direction.Right,false);
                    if (worm.Position.X > x) return new WormGameStep(Direction.Left, false);
                    if (worm.Position.Y > y) return new WormGameStep(Direction.Down, false);
                    if (worm.Position.Y < y) return new WormGameStep(Direction.Up, false);
                }
            }
            else
            {
                var (x, y) = NearestFoodPosition(worm, world.Foods);
                if (worm.Position.X < x) return new WormGameStep(Direction.Right,false);
                if (worm.Position.X > x) return new WormGameStep(Direction.Left, false);
                if (worm.Position.Y > y) return new WormGameStep(Direction.Down, false);
                if (worm.Position.Y < y) return new WormGameStep(Direction.Up, false);
            }

            return new WormGameStep();
        }

        private Direction FindSwapPosition(Worm worm, World world)
        {
            var wormPosition = worm.Position;

            var tryLeft = wormPosition.DirectedPosition(Direction.Left);
            var tryLeftFlag = true;
            foreach (var w in world.Worms)
            {
                if (w.Position.Equals(tryLeft))
                {
                    tryLeftFlag = false;
                }
            }
            
            foreach (var f in world.Foods)
            {
                if (f.Position.Equals(tryLeft))
                {
                    tryLeftFlag = false;
                }
            }

            if (tryLeftFlag)
            {
                return Direction.Left;
            }
            
            var tryRight = wormPosition.DirectedPosition(Direction.Right);
            var tryRightFlag = true;
            foreach (var w in world.Worms)
            {
                if (w.Position.Equals(tryRight))
                {
                    tryRightFlag = false;
                }
            }
            
            foreach (var f in world.Foods)
            {
                if (f.Position.Equals(tryRight))
                {
                    tryRightFlag = false;
                }
            }

            if (tryRightFlag)
            {
                return Direction.Right;
            }
            
            var tryUp = wormPosition.DirectedPosition(Direction.Up);
            var tryUpFlag = true;
            foreach (var w in world.Worms)
            {
                if (w.Position.Equals(tryUp))
                {
                    tryUpFlag = false;
                }
            }
            
            foreach (var f in world.Foods)
            {
                if (f.Position.Equals(tryUp))
                {
                    tryUpFlag = false;
                }
            }

            if (tryUpFlag)
            {
                return Direction.Up;
            }

            return Direction.Down;
        }

        private (int, int) NearestFoodPosition(Worm worm, List<Food> foods)
        {
            int x = 0, y = 0;
            var minDistance = int.MaxValue;
            
            foreach (var food in foods)
            {
                var distance = Math.Abs(food.Position.X - worm.Position.X) +
                               Math.Abs(food.Position.Y - worm.Position.Y);

                if (distance < minDistance && distance < worm.Health)
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