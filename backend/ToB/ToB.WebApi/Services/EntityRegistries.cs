using System.Collections.Generic;
using System.Linq;
using ToB.WebApi.DB;
using ToB.WebApi.Interfaces;

namespace ToB.WebApi.Services
{
    public sealed class EntityRegistries : IRegistries
    {
        private readonly RandomizerContext context;

        public EntityRegistries(RandomizerContext context)
        {
            this.context = context;
        }

        public List<Registry> ToAll(int root)
        {
            return context
                .Registries
                .Where(_ => _.Parent == root)
                .ToList();
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