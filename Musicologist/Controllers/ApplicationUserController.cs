using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserController(ILogger<HomeController> logger,
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

            var applicationUser = _applicationUserRepository.GetUser(userId);

            var model = new ApplicationUserViewModel();

            model.CurrentApplicationUser = _applicationUserRepository.GetUser(userId)
                .Select(x => new ApplicationUserViewModel.ApplicationUser
                {
                    Email = x.Email,
                    Courses = x.Courses.Select(c => new ApplicationUserViewModel.Course { Title = c.Title }).ToList(),
                    UserStatistics = new ApplicationUserViewModel.UserStatistics { XPGainedTotal = x.UserStatistics.XPGainedTotal }
                }).SingleOrDefault();

            return View(model);
        }
    }
}