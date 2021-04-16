using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System.Diagnostics;

namespace Musicologist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            IApplicationUserRepository applicationUserRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);

            var applicationUser = _applicationUserRepository.GetUserProfile(userId);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}