using Microsoft.AspNetCore.Mvc;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonRepository _repository;

        public LessonViewModel Model { get; set; }
        public LessonController(ILessonRepository repository)
        {
            Model = new LessonViewModel();
            _repository = repository;
        }

        public IActionResult Index(int lessonId, int courseId)
        {
            Model = GetLesson(lessonId);

            Model.CourseId = courseId;

            return View(Model);
        }

        private LessonViewModel GetLesson(int lessonId)
        {
            return _repository.GetLesson(lessonId).Select(lesson => new LessonViewModel
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
        }
    }
}