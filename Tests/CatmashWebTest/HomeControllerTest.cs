using Xunit;
using Catmash.Web.Controllers;
using System.Threading.Tasks;
using Catmash.Tests.DataRepositoryTest;
using Catmash.DataRepository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Catmash.Web.Models;
using System.Collections.Generic;
using Catmash.EntityModel;

namespace Catmash.Tests.Web
{
    public class HomeControllerTest : BaseTest
    {
        private ICatmashRepository repository;
        private ILogger<HomeController> logger;

        public HomeControllerTest() : base()
        {
            repository = new CatmashRepository(context);
            logger = new Logger<HomeController>(new LoggerFactory());
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
        public async Task Vote_ShouldReturnViewResult_WithTotalVotesCount()
        {
            // Arrange
            Init();
            var controller = new HomeController(logger, repository);

            // Act
            var result = await controller.Vote();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeVoteViewModel>(viewResult.ViewData.Model);
            Assert.Equal((ulong)20, model.totalVotes);
        }
    }
}
