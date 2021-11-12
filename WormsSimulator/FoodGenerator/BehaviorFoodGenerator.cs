using System;
using System.Collections.Generic;
using CS_lab.BehaviorDatabase;
using CS_lab.BehaviorDatabase.Entities;
using CS_lab.Models;

namespace CS_lab.FoodGenerator
{
    public class BehaviorFoodGenerator : IFoodGenerator
    {
        private readonly List<FoodPosition> _foods;
        
        public BehaviorFoodGenerator(string behavior)
        {
            using (var db = new ApplicationContext())
            {
                _foods = db.GetFoodListByBehavior(behavior);
            }
            
            
        }
        
        public Food GenerateFood(World world, int gameStep)
        {
            var position = new Position(_foods[gameStep].X, _foods[gameStep].Y);
            var food = new Food(position);
            
            return food;
        }
    }
}