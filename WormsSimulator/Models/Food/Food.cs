using System;
using CS_lab.Models;

namespace CS_lab.Models
{
    public class Food
    {
        private readonly string _id;
        private readonly Position _position;
        private int _health;

        public Position Position
        {
            get => _position;
        }

        public int Health
        {
            get => _health;
        }

        public Food(Position position)
        {
            _id = Guid.NewGuid().ToString();
            _health = 10;
            _position = position;
        }

        public void EatFood()
        {
            _health = 0;
        }

        public void DecreaseHealth()
        {
            _health--;
        }
    }
}