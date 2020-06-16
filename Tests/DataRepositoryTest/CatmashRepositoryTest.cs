using Xunit;
using System.Threading.Tasks;
using Catmash.DataRepository;
using Catmash.Tests.Basic;
using Catmash.EntityModel;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Catmash.Tests.DataRepositoryTest
{
    public class DataRepositoryTest : BaseTest
    {
        public DataRepositoryTest() : base() { }

        private void Init()
        {
            IEnumerable<Image> images = new Image[]
            {
                new Image{Id = "foo", Url = "www.foo.com", Score = 452},
                new Image{Id = "bar", Url = "www.bar.com", Score = 120},
                new Image{Id = "baz", Url = "www.baz.com", Score = 987},
                new Image{Id = "qux", Url = "www.qux.com", Score = 120},
                new Image{Id = "flu", Url = "www.flu.com", Score = 98}
            };
            context.Images.AddRange(images);
            context.SaveChanges();
        }

        [Fact]
        public async Task CreateAsync_ImageGiven_ShouldCreateInDatabase()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Image toCreate = new Image { Id = "anId", Url = "www.awebsite.com", Score = 1700 };
            Image retrievedBeforeCreation = context.Images.Where(img => img.Id == "anId").SingleOrDefault();

            // Act
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
            Init();

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
            Init();
            Image toRetrieve = new Image { Id = "bar", Url = "www.bar.com", Score = 120 };

            // Act
            Image retrieved = await repository.RetrieveAsync(toRetrieve.Id);

            // Assert
            Assert.Equal(toRetrieve, retrieved, new ImageComparer());
        }

        [Fact]
        public async Task RetrieveAsync_IndexGiven_ShouldReturnImage()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Init();
            Image toRetrieve = new Image { Id = "foo", Url = "www.foo.com", Score = 452 };

            // Act
            Image retrieved = await repository.RetrieveAsync(3);

            // Assert
            Assert.Equal(toRetrieve, retrieved, new ImageComparer());
        }

        [Fact]
        public async Task UpdateAsync_IdAndImageGiven_ShouldUpdataInDatabase()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Init();
            Image toUpdate = context.Images.Where(img => img.Id == "foo").Single();
            toUpdate.Url = toUpdate.Url + "Random seed";
            toUpdate.Score = ++toUpdate.Score;

            // Act
            Image returned = await repository.UpdateAsync(toUpdate.Id, toUpdate);
            Image updated = context.Images.Where(img => img.Id == "foo").Single();

            // Assert
            Assert.Equal(toUpdate, returned, new ImageComparer());
            Assert.Equal(toUpdate, updated, new ImageComparer());
        }

        [Fact]
        public async Task DeleteAsync_IdGiven_ShouldDeleteInDatabase()
        {
            // Arrange
            CatmashRepository repository = new CatmashRepository(context);
            Init();
            Image toDelete = context.Images.Where(img => img.Id == "foo").Single();

            // Act
            bool? isDeleted = await repository.DeleteAsync(toDelete.Id);
            Image afterDeletion = context.Images.Where(img => img.Id == toDelete.Id).SingleOrDefault();

            // Assert
            Assert.True(isDeleted);
            Assert.Null(afterDeletion);
        }
    }
}