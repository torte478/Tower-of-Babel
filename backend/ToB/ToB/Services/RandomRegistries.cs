using System.Collections.Generic;
using ToB.DB;
using ToB.Interfaces;

namespace ToB.Services
{
    public sealed class RandomRegistries : IRandomRegistries
    {
        private readonly IRegistries origin;
        private readonly IRandomizer randomizer;

        public RandomRegistries(IRegistries origin, IRandomizer randomizer)
        {
            this.origin = origin;
            this.randomizer = randomizer;
        }

        public List<Registry> ToAll(int root)
        {
            return origin.ToAll(root);
        }

        public IRegistries Delete(int id)
        {
            origin.Delete(id);
            return this;
        }

        public IRegistries Add(int? parent, string label)
        {
            origin.Add(parent, label);
            return this;
        }

        public List<Registry> ToRandom(int root)
        {
            var result = new List<Registry>();
            while (true)
            {
                var items = origin.ToAll(root);

                if (items.Count == 0)
                    break;
                
                var item = items[randomizer.ToRandom(0, items.Count - 1)];
                result.Add(item);
                root = item.Id;
            }

            return result;
        }
    }
}