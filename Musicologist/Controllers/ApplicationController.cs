using Microsoft.AspNetCore.Mvc;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Musicologist.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationRepository _repository;

        public CourseViewModel Model;

        public ApplicationController(IApplicationRepository repository)
        {
            _repository = repository;

            Model = new CourseViewModel();
        }

        public IActionResult Index()
        {
            Model.Courses = GetCourses();

            return View(Model);
        }
        public List<CourseViewModel.Course> GetCourses()
        {
            return _repository.GetCourses().Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        public CourseViewModel GetDetails(int courseId)
        {
            return _repository.GetCourseOverview(courseId).Select(course => new CourseViewModel
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