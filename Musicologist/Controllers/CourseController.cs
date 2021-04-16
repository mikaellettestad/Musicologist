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
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ICourseRepository _courseRepository;
        public CourseViewModel Model;

        public CourseController(UserManager<ApplicationUser> userManager, IApplicationUserRepository applicationUserRepository, ICourseRepository courseRepository)
        {
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
            _courseRepository = courseRepository;
            Model = new CourseViewModel();
        }

        public IActionResult Index()
        {
            Model.Courses = GetAllCourses();

            return View(Model);
        }

        public IActionResult CourseDetails(string Id)
        {
            Model.CurrentCourse = GetSingleCourse(Convert.ToInt32(Id));

            return View(Model);
        }

        private List<CourseViewModel.Course> GetAllCourses()
        {
            return _courseRepository.GetCourses().Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        private CourseViewModel.Course GetSingleCourse(int Id)
        {
            return _courseRepository.GetCourse(Id).Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                XP = c.XP,
                ImageUrl = c.ImageUrl
            }).SingleOrDefault();
        }
    }
}
