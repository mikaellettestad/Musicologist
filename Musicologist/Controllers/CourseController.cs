using Microsoft.AspNetCore.Mvc;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Musicologist.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _repository;
        public CourseViewModel Model;

        public CourseController(IApplicationUserRepository applicationUserRepository, ICourseRepository repository)
        {
            _repository = repository;

            Model = new CourseViewModel();
        }

        public IActionResult Index()
        {
            Model.Courses = GetCourses();

            return View(Model);
        }

        public IActionResult Course(int courseId)
        {
            return View(GetCourse(courseId));
        }

        private List<CourseViewModel.Course> GetCourses()
        {
            return _repository.GetCourses().Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        private CourseViewModel GetCourse(int courseId)
        {
            return _repository.GetCourse(courseId).Select(course => new CourseViewModel
            {
                CurrentCourse = new CourseViewModel.Course
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    XPReward = course.XP
                }
            }).SingleOrDefault();
        }
    }
}