using System;
using System.Linq;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToB.Extensions;
using ToB.Interfaces;

namespace ToB.Services
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

        public IQueryable<T> Select<T>(Func<TContext, DbSet<T>> toTable, string sql) where T : class
        {
            log.LogDebug(sql);
            
            return context
                ._(toTable)
                .FromSqlRaw(sql);
        }

        public ISqlRequests<TContext> Change(string sql)
        {
            log.LogDebug(sql);
            
            context.Database.ExecuteSqlRaw(sql);
            return this;
        }
    }
}