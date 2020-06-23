using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catmash.Web.Controllers;
using Catmash.Algorithms;
using Catmash.DataRepository;
using Catmash.Tests.DataRepositoryTest;
using Catmash.Web;
using Catmash.EntityModel;
using Catmash.Web.Models;
using System;

namespace Catmash.Tests.Web
{
    public class CatmashImageControllerTest : BaseTest
    {
        private ICatmashRepository repository;
        private IPairGeneratorStrategy pairGenerator;
        private PairNumberTracker pairNumTracker;
        private EloRatingCalculator eloRatingCalculator;
        private Constants constants;

        public CatmashImageControllerTest()
        {
            this.repository = new CatmashRepository(context);
            this.pairGenerator = new PatternedPairGenerator();
            this.pairNumTracker = new PairNumberTracker();
            this.eloRatingCalculator = new EloRatingCalculator();
            this.constants = new Constants();
        }

        private void Init()
        {
            IEnumerable<Image> images = new Image[]
            {
                new Image{Id = "foo", Url = "www.foo.com", Score = 452.865M, Votes = 2},
                new Image{Id = "bar", Url = "www.bar.com", Score = 120.43M, Votes = 4},
                new Image{Id = "baz", Url = "www.baz.com", Score = 987, Votes = 1},
                new Image{Id = "qux", Url = "www.qux.com", Score = 120, Votes = 3},
                new Image{Id = "flu", Url = "www.flu.com", Score = 98.01M, Votes = 10}
            };
            context.Images.AddRange(images);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetImagePair_ShouldReturnAllPairsAfterEnoughCalls()
        {
            // Arrange
            Init();
            int nbElements = 5;
            int nbPossiblePairs = nbElements * (nbElements - 1);
            var controller = new CatmashImageController(
                repository, pairGenerator, pairNumTracker, eloRatingCalculator, constants);
            var allImages = await repository.RetrieveAllAsync();
            var imageComparer = new ImagePropertiesComparer();
            List<ImagePair> expectedImagePairs = (from img1 in allImages
                                                  from img2 in allImages
                                                  where !imageComparer.Equals(img1, img2)
                                                  orderby img1.Id
                                                  orderby img2.Id
                                                  select new ImagePair { first = img1, second = img2 }).ToList();

            // Act
            List<ImagePair> returnedImagePairs = new List<ImagePair>();
            for (int i = 0; i < nbPossiblePairs; i++)
            {
                returnedImagePairs.Add(await controller.GetImagePair());
            }
            returnedImagePairs = returnedImagePairs.OrderBy(img => img.first.Id).OrderBy(img => img.second.Id).ToList();

            //Assert
            Assert.Equal<ImagePair>(expectedImagePairs, returnedImagePairs, new ImagePairComparer(imageComparer));
        }
    }
}