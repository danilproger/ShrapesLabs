using System;

namespace WorldBehavior.Utils
{
    public static class NormalRandomizer
    {
        private static readonly Random Randomer = new();
        
        public static int NextNormalInt(double mu = 0, double sigma = 1)
        {
            var u1 = Randomer.NextDouble();
            var u2 = Randomer.NextDouble();
            
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);
        }

        public static (int, int) NextNormalXY(double mu = 0, double sigma = 1)
        {
            return (NextNormalInt(mu, sigma), NextNormalInt(mu, sigma));
        }
    }
}