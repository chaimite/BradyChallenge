using BradyChallenge;
using NUnit.Framework;
using System.Collections.Generic;

namespace BradyChallengeTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Day day = new Day { Date = System.DateTime.Now, Energy = 2.1, Price = 1 };
            List<Day> listOfDays = new List<Day>() { day };
            GasGenerator gasGenerator = new GasGenerator()
            {
                EmissionsRating = 2.1,
                Generation = listOfDays,
                Name = "GasGenerator1"
            };
        }
    }
}