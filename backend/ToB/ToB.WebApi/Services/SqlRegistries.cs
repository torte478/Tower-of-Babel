using System.Collections.Generic;
using System.Linq;
using ToB.WebApi.DB;
using ToB.WebApi.Interfaces;

namespace ToB.WebApi.Services
{
    public sealed class SqlRegistries : IRegistries
    {
        private readonly ISqlRequests<RandomizerContext> sql;

        public SqlRegistries(ISqlRequests<RandomizerContext> sql)
        {
            this.sql = sql;
        }

        public List<Registry> ToAll(int root)
        {
            return sql
                .Read(
                    _ => _.Registries,
                    $"SELECT * FROM registry re WHERE re.parent = {root};")
                .ToList();
        }

        public IRegistries Delete(int id)
        {
            sql.Write($"DELETE FROM registry re WHERE re.id = {id};");
            return this;
        }

        public IRegistries Add(int? parent, string label)
        {
            //TODO : sql-injections!
            sql.Write(
                $"INSERT INTO registry (parent, label) VALUES ({parent}, '{label}');");

            return this;
        }
    }
}