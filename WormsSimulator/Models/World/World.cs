using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;

namespace CS_lab.Models
{
    public class World
    {
        private readonly List<Worm> _worms;
        private readonly List<Food> _foods;
        [JsonPropertyName("worms")]
        public List<Worm> Worms
        {
            get => _worms;
        }
        [JsonPropertyName("food")]
        public List<Food> Foods
        {
            get => _foods;
        }

        public World(List<Worm> worms, List<Food> foods)
        {
            _worms = worms;
            _foods = foods;
        }

        public void AddWorm(Worm worm)
        {
            _worms.Add(worm);
        }

        public void AddFood(Food food)
        {
            _foods.Add(food);
        }

        public bool IsWormOnPosition(int x, int y)
        {
            var isWorm = false;

            foreach (var worm in _worms.Where(worm => worm.Position.X == x && worm.Position.Y == y))
            {
                isWorm = true;
            }

            return isWorm;
        }
        
        public bool IsWormOnPosition(Position position)
        {
            return IsWormOnPosition(position.X, position.Y);
        }
        
        
        public bool IsFoodOnPosition(int x, int y)
        {
            var isFood = false;

            foreach (var food in _foods.Where(food => food.Position.X == x && food.Position.Y == y))
            {
                isFood = true;
            }

            return isFood;
        }

        public bool IsFoodOnPosition(Position position)
        {
            return IsFoodOnPosition(position.X, position.Y);
        }
        
        public void CheckWormsFoods()
        {
            foreach (var worm in _worms)
            {
                foreach (var food in _foods)
                {
                    if (worm.Position.X == food.Position.X && worm.Position.Y == food.Position.Y && food.Health != 0)
                    {
                        food.EatFood();
                        worm.IncrementHealth(10);
                    }
                }
            }
        }

    }
}