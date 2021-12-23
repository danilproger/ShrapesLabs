using System;
using System.Text.Json.Serialization;
using CS_lab.Models;

namespace CS_lab.Models
{
    public class Food
    {
        [JsonPropertyName("position")]
        public Position Position { get; }
        [JsonPropertyName("expiresIn")]
        public int Health { get; private set; }

        public Food(Position position)
        {
            Health = 10;
            Position = position;
        }

        public void EatFood()
        {
            Health = 0;
        }

        public void DecreaseHealth()
        {
            Health--;
        }
    }
}