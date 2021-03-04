using System;
using Microsoft.EntityFrameworkCore;

using ToB.Common.DB;

namespace ToB.Common.Extensions
{
    public static class DbCrudExtensions
    {
        public static ICrud<int, TValue> AsCrud<TContext, TValue>(
            this TContext context,
            Func<TContext, DbSet<TValue>> toEntities)
            where TValue : class, IHaveId<int>, ICopyable<TValue>
            where TContext : DbContext
        {
            return new DbCrud<TContext, TValue>(context, toEntities);
        }
    }
}