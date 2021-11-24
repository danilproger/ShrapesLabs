using System.Collections.Generic;

namespace WormsStrategyWebService.Models
{
    public class World
    {
        private readonly List<Worm> _worms;
        private readonly List<Food> _foods;

        public List<Worm> Worms
        {
            get => _worms;
        }
        
        public List<Food> Foods
        {
            get => _foods;
        }
        
        public World(List<Worm> worms, List<Food> foods)
        {
            _worms = worms;
            _foods = foods;
        }
    }
}