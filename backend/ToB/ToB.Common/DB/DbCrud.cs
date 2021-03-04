using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToB.Common.DB
{
    public sealed class DbCrud<TContext, TValue> : ICrud<int, TValue> where TValue : class, IHaveId<int> where TContext : DbContext
    {
        private readonly TContext context;
        private readonly Func<TContext, DbSet<TValue>> getEntities;

        private DbSet<TValue> Entities => getEntities(context);

        public DbCrud(TContext context, Func<TContext, DbSet<TValue>> getEntities)
        {
            this.context = context;
            this.getEntities = getEntities;
        }

        public int Create(TValue item)
        {
            var added = Entities.Add(item);
            context.SaveChanges();
            return added.Entity.Id;
        }

        public IEnumerable<TValue> Read()
        {
            return Entities.ToList();
        }

        public TValue Read(int id)
        {
            return Find(id);
        }

        public bool Update(TValue item)
        {
            var entity = Find(item.Id);
            var exists = entity != null;

            if (exists)
            {
                Entities.Update(item);
                context.SaveChanges();
            }

            return exists;
            
        }

        public bool Delete(int id)
        {
            var entity = Find(id);
            var exists = entity != null;

            if (exists)
            {
                Entities.Remove(entity);
                context.SaveChanges();
            }

            return exists;
        }

        private TValue Find(int id)
        {
            return Entities.FirstOrDefault(_ => _.Id == id);
        }
    }
}