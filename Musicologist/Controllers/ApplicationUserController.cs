﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    [Authorize(Roles = "User")]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _repository;
        public ApplicationUserViewModel Model;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, IApplicationUserRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
            Model = new ApplicationUserViewModel();
        }

        public IActionResult Index()
        {
            Model = GetApplicationUser(_userManager.GetUserId(User));

            return View(Model);
        }

        private ApplicationUserViewModel GetApplicationUser(string applicationUserId)
        {
            var applicationUser = _repository.GetApplicationUser(applicationUserId)
                .Select(x => new ApplicationUserViewModel
                {
                    UserName = x.UserName,
                    XP = x.XP
                }).SingleOrDefault();

            applicationUser.ApplicationUserCourses = _repository.GetApplicationUserCourses(applicationUserId).Select(a => new ApplicationUserViewModel.ApplicationUserCourse
            {
                Id = a.Course.Id,
                Title = a.Course.Title,
                IsCompleted = a.IsCompleted,
                ImageUrl = a.Course.ImageUrl
                
            }).ToList();

            return applicationUser;
        }
    }
}