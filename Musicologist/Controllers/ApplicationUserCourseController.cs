using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Controllers
{
    public class ApplicationUserCourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserCourseRepository _applicationUserCourseRepository;
        public ApplicationUserCourseViewModel Model;

        public ApplicationUserCourseController(UserManager<ApplicationUser> userManager, IApplicationUserCourseRepository applicationUserCourseRepository)
        {
            _userManager = userManager;
            _applicationUserCourseRepository = applicationUserCourseRepository;
            Model = new ApplicationUserCourseViewModel();
        }
        
        public IActionResult Index(int id)
        {
            Model.CurrentApplicationUserCourse = GetCourse(_userManager.GetUserId(User), id);

            return View(Model);
        }

        [Authorize(Roles = "User")]
        public IActionResult AddCourse(int id)
        {
            Model.CurrentApplicationUserCourse = GetCourse(_userManager.GetUserId(User), id);

            // Tills vidare
            return View("Index", Model);
        }

        private ApplicationUserCourseViewModel.ApplicationUserCourse GetCourse(string applicationUserId, int courseId)
        {
            var applicationUserCourse =_applicationUserCourseRepository.GetCourse(applicationUserId, courseId)
                .Select(c => new ApplicationUserCourseViewModel.ApplicationUserCourse {
                    Id = c.Course.Id,
                    Title = c.Course.Title,
                    Description = c.Course.Description,
                    XP = c.Course.XP,
                    ImageUrl = c.Course.ImageUrl,
                    IsCompleted = false,
                    CourseParts = c.Course.CourseParts.Select(c => new ApplicationUserCourseViewModel.CoursePart
                    {
                        Id = c.Id,
                        Title = c.Description,
                        Description = c.Description,
                        Lessons = c.Lessons.Select(l => new ApplicationUserCourseViewModel.Lesson
                        {
                            Id = l.Id,
                            Title = l.Title,
                            Description = l.Description,
                            Assignment = new ApplicationUserCourseViewModel.Assignment
                            {
                                Id = l.Assignment.Id,
                                IsCompleted = false
                            }
                        }).ToList()
                    }).ToList()
            }).SingleOrDefault();

            foreach (var coursePart in applicationUserCourse.CourseParts)
            {
                foreach (var lesson in coursePart.Lessons)
                {
                    var assignment = _applicationUserCourseRepository.GetAssignment(applicationUserId, lesson.Assignment.Id).SingleOrDefault();
                    
                    if (assignment != null)
                    {
                        if (assignment.IsCompleted)
                        {
                            lesson.Assignment.IsCompleted = true;
                        }
                    }
                }
            }

            // Istället kanske skapa en räknare istället. Ex 3 stycken är avklarade. Borde vara mer effektivt

            return applicationUserCourse;
        }
    }
}
