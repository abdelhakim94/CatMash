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
        public async Task CreateAsync_ImageGiven_ShouldCreateInDatabase()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Image toCreate = new Image { Id = "anId", Url = "www.awebsite.com", Score = 1700 };

            // Act
            Image retrievedBeforeCreation = context.Images.Where(img => img.Id == "anId").SingleOrDefault();
            Image returned = await repository.CreateAsync(toCreate);
            Image retrievedAfterCreation = context.Images.Where(img => img.Id == "anId").Single();

            // Assert
            Assert.Null(retrievedBeforeCreation);
            Assert.Equal(toCreate, returned, new ImageComparer());
            Assert.Equal(toCreate, retrievedAfterCreation, new ImageComparer());
        }

        [Fact]
        public async Task RetrieveAllAsync_ShouldReturnAllImages()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);

            // Act
            var retrievedImages = await repository.RetrieveAllAsync();

            // Assert
            Assert.True(retrievedImages.SequenceEqual(context.Images, new ImageComparer()));
        }

        [Fact]
        public async Task RetrieveAsync_IdGiven_ShouldReturnImage()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Image toRetrieve = new Image { Id = "anId", Url = "www.awebsite.com", Score = 325 };
            context.Images.Add(toRetrieve);
            await context.SaveChangesAsync();

            // Act
            Image retrieved = await repository.RetrieveAsync(toRetrieve.Id);

            // Assert
            Assert.Equal(toRetrieve, retrieved, new ImageComparer());
        }
    }
}