using System.Collections.Generic;
using System.Linq;
using ToB.DB;
using ToB.Interfaces;

namespace ToB.Services
{
    public sealed class EntityRegistries : IRegistries
    {
        private readonly RandomizerContext context;
        private readonly IRandomizer randomizer;

        public EntityRegistries(RandomizerContext context, IRandomizer randomizer)
        {
            this.context = context;
            this.randomizer = randomizer;
        }

        public List<Registry> ToAll(int root)
        {
            return context
                .Registries
                .Where(_ => _.Parent == root)
                .ToList();
        }

        public List<Registry> ToRandom(int root)
        {
            var result = new List<Registry>();
            while (true)
            {
                var items = ToAll(root);

                if (items.Count == 0)
                    break;
                
                var item = items[randomizer.ToRandom(0, items.Count - 1)];
                result.Add(item);
                root = item.Id;
            }

            return result;
        }

        public IRegistries Delete(int id)
        {
            var item = context.Registries.FirstOrDefault(_ => _.Id == id);
            
            if (item != null)
            {
                context.Registries.Remove(item);
                context.SaveChanges();
            }

            return this;
        }

        public IRegistries Add(int? parent, string label)
        {
            context.Registries.Add(new Registry
            {
                Label = label,
                Parent = parent
            });
            context.SaveChanges();
            
            return this;
        }
    }
}