using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services;
using Musicologist.Services.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class ApplicationUserCourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserCourseRepository _repository;
        public ApplicationUserCourseViewModel Model;

        public ApplicationUserCourseController(UserManager<ApplicationUser> userManager, IApplicationUserCourseRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
            Model = new ApplicationUserCourseViewModel();
        }
        
        public IActionResult Index(int courseId)
        {
            Model.CurrentApplicationUserCourse = GetApplicationUserCourse(_userManager.GetUserId(User), courseId);

            Model.CurrentCourseId = courseId;

            return View(Model);
        }

        public IActionResult Details(int courseId)
        {
            var CourseIsAdded = CheckIfCourseIsAdded(_userManager.GetUserId(User), courseId);

            var model = GetDetails(courseId);

            if (CourseIsAdded)
                model.CurrentApplicationUserCourse.IsAdded = true;
            else
                model.CurrentApplicationUserCourse.IsAdded = false;

            return View(model);
        }

        private ApplicationUserCourseViewModel GetDetails(int courseId)
        {
            return _repository.GetCourseOverview(courseId).Select(course => new ApplicationUserCourseViewModel
            {
                CurrentApplicationUserCourse = new ApplicationUserCourseViewModel.ApplicationUserCourse
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    XPReward = course.XP
                }
            }).SingleOrDefault();
        }

        [Authorize(Roles = "User")]
        public IActionResult AddCourse(int courseId)
        {
            var CourseIsAdded = CheckIfCourseIsAdded(_userManager.GetUserId(User), courseId);

            if (CourseIsAdded)
            {
                Model.CurrentApplicationUserCourse = GetApplicationUserCourse(_userManager.GetUserId(User), courseId);

                return View("Index", Model);
            }

            _repository.AddApplicationUserCourse(_userManager.GetUserId(User), courseId);

            Model.CurrentApplicationUserCourse = GetApplicationUserCourse(_userManager.GetUserId(User), courseId);

            Model.CurrentCourseId = courseId;

            return View("Index", Model);
        }

        private bool CheckIfCourseIsAdded(string applicationUserId, int courseId)
        {
            var result =_repository.GetCourseDetails(applicationUserId, courseId).SingleOrDefault();

            if (result != null)
                return true;
            else
                return false;
        }

        private ApplicationUserCourseViewModel.ApplicationUserCourse GetApplicationUserCourse(string applicationUserId, int courseId)
        {
            var applicationUserCourse =_repository.GetCourse(applicationUserId, courseId)
                .Select(c => new ApplicationUserCourseViewModel.ApplicationUserCourse {
                    XPEarned = c.XPEarned,
                    Id = c.Course.Id,
                    Title = c.Course.Title,
                    Description = c.Course.Description,
                    XPReward = c.Course.XP,
                    ImageUrl = c.Course.ImageUrl,
                    IsCompleted = c.IsCompleted,
                    AssignmentsCompleted = c.AssignmentsCompleted,
                    CourseParts = c.Course.CourseParts.Select(c => new ApplicationUserCourseViewModel.CoursePart
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        Lessons = c.Lessons.Select(l => new ApplicationUserCourseViewModel.Lesson
                        {
                            Id = l.Id,
                            Title = l.Title,
                            Description = l.Description,
                        }).ToList()
                    }).ToList()
            }).SingleOrDefault();

            IApplicationUserCourseService _service = new ApplicationUserCourseService();

            applicationUserCourse.NumberOfLessons = _service.GetNumberOfLessons(applicationUserCourse);

            return applicationUserCourse;
        }
    }
}