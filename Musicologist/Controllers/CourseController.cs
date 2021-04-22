using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services;
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
            Model.Courses = GetAllCourseDetails();

            return View(Model);
        }

        public IActionResult CourseDetails(string Id)
        {
            Model.CurrentCourse = GetCourseDetails(Convert.ToInt32(Id));

            return View(Model);
        }

        public IActionResult Course(int id)
        {
            Model.CurrentCourse = GetCourse(id);

            // Tills vidare
            return View(Model);
        }

        [Authorize(Roles = "User")]
        public IActionResult AddCourse(int id)
        {
            Model.CurrentCourse = GetCourse(id);

            // Tills vidare
            return View("Course", Model);
        }

        public IActionResult ApplicationUserCourse()
        {
            string ApplicationUserId = _userManager.GetUserId(User);

            return View();
        }

        [HttpPost]
        public IActionResult ApplicationUserCourse(ApplicationUserViewModel model)
        {
            Model.CurrentCourse = GetCourse(Convert.ToInt32(model.CurrentCourse.Id));

            string ApplicationUserId = _userManager.GetUserId(User);

            return View(Model);
        }

        public IActionResult Lesson(int Id)
        {
            Model.CurrentLesson = GetLesson(Convert.ToInt32(Id));

            return View(Model);
        }

        //public IActionResult Assignment(int assignmentId)
        //{
        //    Model.CurrentLesson = GetLesson(Convert.ToInt32(assignmentId));

        //    return View(Model);
        //}

        //[HttpPost]
        //public IActionResult Assignment(CourseViewModel model)
        //{
        //    Model.CurrentLesson = GetLesson(Convert.ToInt32(model.CurrentLesson.Id));

        //    if (model.CurrentAnswer.IsCorrect)
        //    {
        //        var assignmentService = new AssignmentService();

        //        assignmentService.Register(_userManager.GetUserId(User), model.CurrentAssignment.Id);

        //        return View(Model);
        //    }

        //    return View(Model);
        //}

        private List<CourseViewModel.Course> GetAllCourseDetails()
        {
            return _courseRepository.GetAllCourseDetails().Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        private CourseViewModel.Course GetCourseDetails(int Id)
        {
            return _courseRepository.GetCourseDetails(Id).Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                XP = c.XP,
                ImageUrl = c.ImageUrl
            }).SingleOrDefault();
        }

        private CourseViewModel.Course GetCourse(int Id)
        {
            return _courseRepository.GetCourse(Id).Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                XP = c.XP,
                CourseParts = c.CourseParts.Select(c => new CourseViewModel.CoursePart {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Lessons = c.Lessons.Select(c => new CourseViewModel.Lesson { 
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        Assignment = new CourseViewModel.Assignment
                        {
                            IsCompleted = false
                        }
                    }).ToList()
                }).ToList()
            }).SingleOrDefault();
        }


        private CourseViewModel.Lesson GetLesson(int Id)
        {
            return _courseRepository.GetLesson(Id).Select(l => new CourseViewModel.Lesson
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                LessonTexts = l.LessonTexts.Select(l => new CourseViewModel.LessonText {
                    Title = l.Title,
                    Text = l.Text
                }).ToList(),
                LessonImages = l.LessonImages.Select(l => new CourseViewModel.LessonImage
                {
                    Id = l.Id,
                    Title = l.Title,
                    ImageUrl = l.ImageUrl
                }).ToList(),
                Assignment = new CourseViewModel.Assignment()
                { 
                    Id = l.Assignment.Id,
                    Title = l.Assignment.Title,
                    Question = l.Assignment.Question,
                    XPRewardIfCompleted = l.Assignment.XPRewardIfCompleted,
                    IsCompleted = false,
                    Answers = l.Assignment.Answers.Select(a => new CourseViewModel.Answer
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }
            }).SingleOrDefault();
        }
    }
}