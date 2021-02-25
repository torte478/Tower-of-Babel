using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToB.Interfaces
{
    public interface ISqlRequests<out TContext> where TContext : DbContext
    {
        IQueryable<T> Read<T>(Func<TContext, DbSet<T>> toTable, string sql) 
            where T : class;

        ISqlRequests<TContext> Write(string sql);
    }
}