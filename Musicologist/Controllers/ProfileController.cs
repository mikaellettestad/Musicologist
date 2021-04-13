using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Types;
using Musicologist.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Musicologist.Controllers
{
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public ApplicationUserViewModel Model;

        public ProfileController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            IApplicationUserRepository applicationUserRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
            Model = new ApplicationUserViewModel();
        }

        public IActionResult Index()
        {
            Model.CurrentApplicationUser = GetProfile();

            return View(Model);
        }
        public IActionResult Edit()
        {
            Model.CurrentApplicationUser = GetProfile();

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUserViewModel applicationUserViewModel)
        {
            var result = UpdateProfile(applicationUserViewModel.CurrentApplicationUser);

            if(result.Result == Update.Succeeded)
            {
                Model.CurrentApplicationUser = GetProfile();

                Model.ConfirmationMessage = "Saved profile";

                return View(Model);
            }

            Model.CurrentApplicationUser = GetProfile();

            Model.ErrorMessage = result.Result.ToString();

            return View(Model);
        }

        private ApplicationUserViewModel.ApplicationUser GetProfile()
        {
            return _applicationUserRepository.GetUser(_userManager.GetUserId(User))
                .Select(x => new ApplicationUserViewModel.ApplicationUser
                {
                    Email = x.Email,
                    Courses = x.Courses.Select(c => new ApplicationUserViewModel.Course { Title = c.Title }).ToList(),
                    UserStatistics = new ApplicationUserViewModel.UserStatistics { XPGainedTotal = x.UserStatistics.XPGainedTotal }
                }).SingleOrDefault();
        }

        private async Task<Update> UpdateProfile(ApplicationUserViewModel.ApplicationUser currentApplicationUser)
        {
            var applicationUser = _applicationUserRepository.GetUser(_userManager.GetUserId(User)).SingleOrDefault();

            applicationUser.UserName = currentApplicationUser.Email;

            applicationUser.Email = currentApplicationUser.Email;

            var result = await _userManager.UpdateAsync(applicationUser);

            if (result.Succeeded)
            {
                return Update.Succeeded;
            }

            return Update.Failed;
        }
    }
}