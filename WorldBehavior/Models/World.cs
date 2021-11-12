using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldBehavior.Models
{
    public class World
    {
        private readonly List<Tuple<FoodPosition, int>> _foods;

        public World()
        {
            _foods = new List<Tuple<FoodPosition, int>>();
        }

        public void AddFood(FoodPosition foodPosition)
        {
            _foods.Add(new Tuple<FoodPosition, int>(foodPosition, 10));
        }

        public void DecrementFood()
        {
            for (int i = 0; i < _foods.Count; i++)
            {
                _foods[i] = new Tuple<FoodPosition, int>(_foods[i].Item1, _foods[i].Item2 - 1);
            }
        }

        public bool IsFoodOnPosition(int x, int y)
        {
            var isFoodOnPosition = false;

            foreach (var food in _foods)
            {
                if (food.Item1.X == x && food.Item1.Y == y && food.Item2 > 0)
                {
                    isFoodOnPosition = true;
                }
            }

            return isFoodOnPosition;
        }

        public List<FoodPosition> GetFoodPositions()
        {
            return _foods.Select(tuple => tuple.Item1).ToList();
        }
    }
}