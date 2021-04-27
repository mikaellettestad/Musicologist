using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    [Authorize(Roles = "User")]
    public class ApplicationUserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public ApplicationUserViewModel Model;

        public ApplicationUserController(ILogger<HomeController> logger,
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
            Model.CurrentApplicationUser = GetApplicationUser();

            return View(Model);
        }
        //public IActionResult Edit()
        //{
        //    Model.CurrentApplicationUser = GetApplicationUser();

        //    return View(Model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(ApplicationUserViewModel applicationUserViewModel)
        //{
        //    var result = UpdateProfile(applicationUserViewModel.CurrentApplicationUser);

        //    if(result.Result == Update.Succeeded)
        //    {
        //        Model.CurrentApplicationUser = GetApplicationUser();

        //        Model.ConfirmationMessage = "Saved profile";

        //        return View(Model);
        //    }

        //    Model.CurrentApplicationUser = GetApplicationUser();

        //    Model.ErrorMessage = result.Result.ToString();

        //    return View(Model);
        //}

        private ApplicationUserViewModel.ApplicationUser GetApplicationUser()
        {
            string Id = _userManager.GetUserId(User);

            var applicationUser = _applicationUserRepository.GetApplicationUser(_userManager.GetUserId(User))
                .Select(x => new ApplicationUserViewModel.ApplicationUser
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    UserStatistics = new ApplicationUserViewModel.UserStatistics { 
                      XPGainedTotal = x.UserStatistics.XPGainedTotal 
                    }
                }).SingleOrDefault();

            applicationUser.ApplicationUserCourses = _applicationUserRepository.GetApplicationUserCourses(Id).Select(a => new ApplicationUserViewModel.ApplicationUserCourse
            {
                Id = a.Course.Id,
                Title = a.Course.Title,
                IsCompleted = a.IsCompleted
            }).ToList();


            return applicationUser;
        }

        //private async Task<Update> UpdateProfile(ApplicationUserViewModel.ApplicationUser currentApplicationUser)
        //{
        //    var applicationUser = _applicationUserRepository.GetApplicationUser(_userManager.GetUserId(User)).SingleOrDefault();

        //    applicationUser.UserName = currentApplicationUser.Email;

        //    applicationUser.Email = currentApplicationUser.Email;

        //    var result = await _userManager.UpdateAsync(applicationUser);

        //    if (result.Succeeded)
        //    {
        //        return Update.Succeeded;
        //    }

        //    return Update.Failed;
        //}
    }
}