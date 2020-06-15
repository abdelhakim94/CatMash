using Xunit;
using System.Threading.Tasks;
using Catmash.DataRepository;
using Catmash.Tests.Basic;
using Catmash.EntityModel;
using System.Linq;

namespace Catmash.DataRepositoryTest
{
    public class DataRepositoryTest : BaseTest
    {
        public DataRepositoryTest() : base() { }

        [Fact]
        public async Task CreateAsync_Image()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Image toCreate = new Image { Id = "anId", Url = "www.awebsite.com", Score = 1700 };

            // Act
            Image returned = await repository.CreateAsync(toCreate);
            Image created = this.context.Images.Where(img => img.Id == "anId").Single();

            // Assert
            Assert.Equal(toCreate, returned, new ImageComparer());
            Assert.Equal(toCreate, created, new ImageComparer());
        }
    }
}