using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catmash.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Vote()
        {
            return View();
        }
    }
}
