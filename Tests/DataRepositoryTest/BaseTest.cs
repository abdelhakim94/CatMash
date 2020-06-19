using System;
using Microsoft.EntityFrameworkCore;
using Catmash.EntityModel;

namespace Catmash.Tests.Basic
{
    public class BaseTest : IDisposable
    {
        protected readonly CatmashEntities context;

        public BaseTest()
        {
            var options = new DbContextOptionsBuilder<CatmashEntities>()
                .UseSqlite("DataSource=:memory:", x => { })
                .Options;
            context = new CatmashEntities(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }

        public virtual void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Database.CloseConnection();
            context.Dispose();
        }
    }
}