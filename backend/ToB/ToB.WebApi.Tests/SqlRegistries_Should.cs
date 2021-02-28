using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using ToB.WebApi.DB;
using ToB.WebApi.Interfaces;

namespace ToB.WebApi.Services.Tests
{
    [TestFixture]
    internal sealed class SqlRegistries_Should
    {
        private FakeSqlRequests requests;
        private SqlRegistries registries;

        [SetUp]
        public void SetUp()
        {
            requests = new FakeSqlRequests();
            registries = new SqlRegistries(requests);
        }
        
        [Test]
        public void AddSpaces_WhenBuildSql()
        {
            registries.ToAll(3);
            
            Assert.That(requests.Last.Contains("SELECT * FROM"), Is.True);
        }

        [Test]
        public void AddSemicolon_ToDeleteRequest()
        {
            registries.Delete(4);
            
            Assert.That(requests.Last.EndsWith(';'), Is.True);
        }

        [Test]
        public void AddId_ToDeleteRequest()
        {
            registries.Delete(4);
            
            Assert.That(requests.Last.Contains("id"), Is.True);
        }

        [Test]
        public void AddQuotes_ToInsertRequest()
        {
            registries.Add(4, "Some text");
            
            Assert.That(requests.Last.Contains("'Some text'"), Is.True);
        }
        
        private sealed class FakeSqlRequests : ISqlRequests<RandomizerContext>
        {
            public string Last { get; private set; }
            
            public IQueryable<T> Read<T>(Func<RandomizerContext, DbSet<T>> toTable, string sql) where T : class
            {
                Last = sql;
                return Enumerable.Empty<T>().AsQueryable();
            }

            public ISqlRequests<RandomizerContext> Write(string sql)
            {
                Last = sql;
                return this;
            }
        }
    }
}