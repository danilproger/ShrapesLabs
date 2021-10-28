using System;
using System.Collections.Generic;
using System.Linq;
using CS_lab.FoodGenerator;
using CS_lab.NameGenerator;
using NUnit.Framework;

namespace WormsTests
{
    public class NamesGeneratorTests
    {
        private INameGenerator _nameGenerator;
        private List<string> _names;

        [SetUp]
        public void SetUp()
        {
            _nameGenerator = new RandomNameGenerator();
            _names = new List<string>();
        }

        [Test]
        public void RandomUniqueNames()
        {
            for (int i = 0; i < 100; ++i)
            {
                _names.Add(_nameGenerator.NextName());
            }
            
            Assert.IsTrue(_names.Distinct().Count() == _names.Count);
        }
    }
}