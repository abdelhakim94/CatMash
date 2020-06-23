using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catmash.DataRepository;
using System.Threading.Tasks;
using System.Linq;
using Catmash.Web.Models;

namespace Catmash.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICatmashRepository repository;

        public HomeController(ILogger<HomeController> logger, ICatmashRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<IActionResult> Vote()
        {
            var model = new HomeVoteViewModel
            {
                totalVotes = (await repository.RetrieveAllAsync())
                    .Aggregate(ulong.MinValue, (nbVotes, image) => nbVotes += image.Votes)
            };
            return View(model);
        }

        public async Task<IActionResult> Scores()
        {
            var model = new HomeScoresViewModel()
            {
                images = (await repository.RetrieveAllAsync()).OrderByDescending(img => img.Score)
            };
            return View(model);
        }
    }
}
