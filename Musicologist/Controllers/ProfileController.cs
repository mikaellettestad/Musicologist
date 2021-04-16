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
            string Id = _userManager.GetUserId(User);

            var applicationUser = _applicationUserRepository.GetUserProfile(Id)
                .Select(x => new ApplicationUserViewModel.ApplicationUser
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    UserStatistics = new ApplicationUserViewModel.UserStatistics { 
                        XPGainedTotal = x.UserStatistics.XPGainedTotal 
                    }
                }).SingleOrDefault();

            applicationUser.Courses = _applicationUserRepository.GetUserCourses(Id).Select(x => new ApplicationUserViewModel.Course
            {
                CourseId = x.Course.Id,
                Title = x.Course.Title
            }).ToList();

            return applicationUser;
        }

        private async Task<Update> UpdateProfile(ApplicationUserViewModel.ApplicationUser currentApplicationUser)
        {
            var applicationUser = _applicationUserRepository.GetUserProfile(_userManager.GetUserId(User)).SingleOrDefault();

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