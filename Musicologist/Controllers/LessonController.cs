using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class LessonController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILessonRepository _repository;

        public LessonViewModel Model { get; set; }
        public LessonController(UserManager<ApplicationUser> userManager, ILessonRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
            Model = new LessonViewModel();
        }

        public IActionResult Index(int lessonId, int courseId)
        {
            Model = GetLesson(_userManager.GetUserId(User), lessonId);

            Model.CourseId = courseId;

            return View(Model);
        }

        //Service-klass
        private LessonViewModel GetLesson(string applicationUserId, int lessonId)
        {
            var model = _repository.GetLesson(lessonId).Select(lesson => new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Texts = lesson.LessonTexts.Select(lesson => new LessonViewModel.LessonText
                {
                    Title = lesson.Title,
                    Text = lesson.Text,
                }).ToList(),
                Images = lesson.LessonImages.Select(lesson => new LessonViewModel.LessonImage
                {
                    Title = lesson.Title,
                    ImageUrl = lesson.ImageUrl
                }).ToList(),
                AssignmentId = lesson.Assignment.Id
            }).SingleOrDefault();

            var applicationUserAssignment = _repository.GetApplicationUserAssignment(applicationUserId, model.AssignmentId).SingleOrDefault();

            if (applicationUserAssignment != null)
                model.IsCompleted = true;
            else
                model.IsCompleted = false;

            return model;
        }
    }
}