using System;

using ToB.Interfaces;

namespace ToB.Services
{
    public sealed class Randomizer : IRandomizer
    {
        private readonly Random random;

        public Randomizer(Random random)
        {
            this.random = random;
        }

        public int ToRandom(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
