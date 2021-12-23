using System;
using System.Text.Json.Serialization;

namespace WormsStrategyWebService.Models
{
    public class Worm
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("lifeStrength")]
        public int Health { get; set; }
        [JsonPropertyName("position")]
        public Position Position { get; set; }
    }
}