using System;

using FakeItEasy;
using NUnit.Framework;

namespace ToB.Services.Tests
{
    public class Randomizer_Should
    {
        [Test]
        public void CorrectSetMaxValue_WhenReturnRandomAtInterval()
        {
            var fake = A.Fake<Random>();
            var randomizer = new Randomizer(fake);

            randomizer.ToRandom(1, 4);

            A.CallTo(() => fake.Next(A<int>._, 5)).MustHaveHappened();
        }
    }
}