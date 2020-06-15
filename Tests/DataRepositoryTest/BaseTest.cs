using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Catmash.EntityModel;

namespace Catmash.DataRepositoryTest
{
    public class BaseTest : IDisposable
    {
        protected readonly CatmashEntities context;

        protected BaseTest()
        {
            var options = new DbContextOptionsBuilder<CatmashEntities>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            this.context = new CatmashEntities(options);
            context.Database.EnsureCreated();
            var images = new[]
            {
                new Image{Id = "qwd", Url = "foo.com", Score = 10},
                new Image{Id = "sdfg", Url = "bar.com", Score = 35},
                new Image{Id = "thet", Url = "baz.com", Score = 350},
                new Image{Id = "mgk", Url = "qux.com", Score = 200},
                new Image{Id = "aetpl4", Url = "flu.com", Score = 1010}
            };
            context.Images.AddRange(images);
            context.SaveChanges();
        }

        public virtual void Dispose()
        {
            context.Database.EnsureDeleted();
            this.context.Dispose();
        }
    }
}