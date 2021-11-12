using System;
using System.Linq;

namespace CS_lab.NameGenerator
{
    public class RandomNameGenerator : INameGenerator
    {
        private readonly Random _randomer;
        private readonly int _nameLength;

        public RandomNameGenerator(int length = 4)
        {
            _nameLength = 4;
            _randomer = new Random();
        }

        public string NextName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, _nameLength)
                .Select(s => s[_randomer.Next(s.Length)]).ToArray());
        }
    }
}