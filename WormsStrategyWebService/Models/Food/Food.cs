using System;
using System.Text.Json.Serialization;

namespace WormsStrategyWebService.Models
{
    public class Food
    {
        [JsonPropertyName("position")]
        public Position Position { get; set; }
        
        [JsonPropertyName("expiresIn")]
        public int Health { get; set; }
    }
}