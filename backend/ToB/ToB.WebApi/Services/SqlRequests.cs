using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToB.Common.Extensions;
using ToB.WebApi.Interfaces;

namespace ToB.WebApi.Services
{
    public sealed class SqlRequests<TContext> : ISqlRequests<TContext> where TContext : DbContext
    {
        private readonly TContext context;
        private readonly ILogger<SqlRequests<TContext>> log;

        public SqlRequests(TContext context, ILogger<SqlRequests<TContext>> log)
        {
            this.context = context;
            this.log = log;
        }

        public IQueryable<T> Read<T>(Func<TContext, DbSet<T>> toTable, string sql) where T : class
        {
            log.LogDebug("{Sql}", sql);
            
            return context
                ._(toTable)
                .FromSqlRaw(sql);
        }

        public ISqlRequests<TContext> Write(string sql)
        {
            log.LogDebug(sql);
            
            context.Database.ExecuteSqlRaw(sql);
            return this;
        }
    }
}