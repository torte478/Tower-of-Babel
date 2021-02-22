using System.Collections.Generic;
using System.Linq;

using ToB.DB;
using ToB.Interfaces;

namespace ToB.Services
{
    public sealed class SqlRegistries : IRegistries
    {
        private readonly ISqlRequests<RandomizerContext> requests;

        public SqlRegistries(ISqlRequests<RandomizerContext> requests)
        {
            this.requests = requests;
        }

        public List<Registry> ToAll(int root)
        {
            return requests
                .Select(
                    _ => _.Registries,
                    $"SELECT * FROM registry re WHERE re.parent = {root};")
                .ToList();
        }

        public IRegistries Delete(int id)
        {
            requests.Change($"DELETE FROM registry re WHERE re.id = {id};");
            return this;
        }

        public IRegistries Add(int? parent, string label)
        {
            //TODO : sql-injections!
            requests.Change(
                $"INSERT INTO registry (parent, label) VALUES ({parent}, '{label}');");

            return this;
        }
    }
}