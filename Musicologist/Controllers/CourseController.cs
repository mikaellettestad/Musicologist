using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services;
using Musicologist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Musicologist.Controllers
{
    public class CourseController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ICourseRepository _courseRepository;
        public CourseViewModel Model;

        public CourseController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IApplicationUserRepository applicationUserRepository, ICourseRepository courseRepository)
        {
            _signInManager = signInManager;
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

        public IActionResult CourseDetails(int courseId)
        {
            return View(GetCourseDetails(courseId));
        }

        public IActionResult Course(int id)
        {
            Model.CurrentCourse = GetCourse(id);

            return View(Model);
        }

        public IActionResult Lesson(int lessonId, int courseId)
        {
            Model.CurrentLesson = GetLesson(lessonId);

            Model.CurrentCourseId = courseId;

            return View(Model);
        }

        public IActionResult Assignment(int assignmentId)
        {
            Model.CurrentLesson = GetLesson(Convert.ToInt32(assignmentId));

            return View(Model);
        }

        [HttpPost]
        public IActionResult Assignment(CourseViewModel model)
        {
            Model.CurrentLesson = GetLesson(Convert.ToInt32(model.CurrentLesson.Id));

            if (model.CurrentAnswer.IsCorrect)
            {
                var assignmentService = new ApplicationUserAssignmentService();

                assignmentService.Register(_userManager.GetUserId(User), model.CurrentAssignment.Id);

                return View(Model);
            }

            return View(Model);
        }

        private List<CourseViewModel.Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourseDetails().Select(c => new CourseViewModel.Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        private CourseDetailsViewModel GetCourseDetails(int Id)
        {
            return _courseRepository.GetCourseDetails(Id).Select(c => new CourseDetailsViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                XPReward = c.XP,
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
                    XPRewardIfCompleted = l.Assignment.XPReward,
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