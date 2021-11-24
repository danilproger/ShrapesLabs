using System;

namespace WormsStrategyWebService.Models
{
    public class Worm
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Position Position { get; set; }
    }
}