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
        
        public IActionResult Index(int courseId)
        {
            Model.CurrentApplicationUserCourse = GetApplicationUserCourse(_userManager.GetUserId(User), courseId);

            Model.CurrentCourseId = courseId;

            return View(Model);
        }

        public IActionResult CourseDetails(int courseId)
        {
            var CourseIsAdded = CheckIfCourseIsAdded(_userManager.GetUserId(User), courseId);

            var model = new CourseDetailsViewModel();

            model = GetCourseDetails(courseId);

            if (CourseIsAdded)
                model.IsAdded = true;
            else
                model.IsAdded = false;

            return View(model);
        }



        [Authorize(Roles = "User")]
        public IActionResult AddCourse(int courseId)
        {

            // Det som ska hända här
            // Vad behövs för att en kurs ska kunna läggas till?
            // Jo, att en ApplicationUserCourse med kurs och användarId sparas i databasen
            // För det behövs anrop till ApplicationUserCourseRepopsitory

            var CourseIsAdded = CheckIfCourseIsAdded(_userManager.GetUserId(User), courseId);

            if (CourseIsAdded)
            {
                Model.CurrentApplicationUserCourse = GetApplicationUserCourse(_userManager.GetUserId(User), courseId);

                return View("Index", Model);
            }

            _applicationUserCourseRepository.AddApplicationUserCourse(_userManager.GetUserId(User), courseId);

            Model.CurrentApplicationUserCourse = GetApplicationUserCourse(_userManager.GetUserId(User), courseId);
            // Tills vidare
            return View("Index", Model);

        }

        private bool CheckIfCourseIsAdded(string applicationUserId, int courseId)
        {
            var result =_applicationUserCourseRepository.GetApplicationUserCourse(applicationUserId, courseId).SingleOrDefault();

            if (result != null)
                return true;
            else
                return false;
        }

        private CourseDetailsViewModel GetCourseDetails(int courseId)
        {
            return _applicationUserCourseRepository.GetCourseDetails(courseId).Select(c => new CourseDetailsViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                XPReward = c.XP

            }).SingleOrDefault();
        }

        // Felhantering?
        private ApplicationUserCourseViewModel.ApplicationUserCourse GetApplicationUserCourse(string applicationUserId, int courseId)
        {
            var applicationUserCourse =_applicationUserCourseRepository.GetCourse(applicationUserId, courseId)
                .Select(c => new ApplicationUserCourseViewModel.ApplicationUserCourse {
                    XPEarned = c.XPEarned,
                    Id = c.Course.Id,
                    Title = c.Course.Title,
                    Description = c.Course.Description,
                    XPReward = c.Course.XP,
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
