using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssignmentRepository _repository;
        private readonly IAssignmentService _service;
        public AssignmentViewModel Model;
        public AssignmentController(UserManager<ApplicationUser> userManager, IAssignmentRepository repository, IAssignmentService service)
        {
            _userManager = userManager;
            _repository = repository;
            _service = service;
            Model = new AssignmentViewModel();
        }

        //Skapa en SetState()
        public IActionResult Index(int assignmentId, int courseId, int nextLessonId, int nextLessonIndex, bool isLast, int numberOfLessons)
        {
            Model.CurrentAssignment = GetApplicationUserAssignment(assignmentId);

            Model.CurrentCourseId = courseId;
             
            Model.AnswerIsCorrect = false;

            Model.AnswerIsIncorrect = false;

            Model.NextLessonId = nextLessonId;

            Model.NextLessonIndex = nextLessonIndex;

            Model.NumberOfLessons = numberOfLessons;

            Model.IsLast = isLast;

            if(numberOfLessons == nextLessonIndex)
            {
                Model.IsLast = true;
            }

            return View(Model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Model.CurrentAssignment = GetApplicationUserAssignment(model.CurrentAssignment.Id);

                Model.CurrentCourseId = model.CurrentCourseId;

                Model.NextLessonId = model.NextLessonId;

                Model.NextLessonIndex = model.NextLessonIndex;

                Model.NumberOfLessons = model.NumberOfLessons;

                if (model.CurrentAnswer.IsCorrect)
                {
                    _service.AddResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id, true);

                    Model.AnswerIsCorrect = true;
                    Model.AnswerIsIncorrect = false;


                }
                else
                {
                    Model.AnswerIsCorrect = false;
                    Model.AnswerIsIncorrect = true;
                }
            }

            if (model.IsLast && model.CurrentAnswer.IsCorrect)
            {
                return Redirect("/ApplicationUserCourse/Index?courseId=" + model.CurrentCourseId);
            }

            return View(Model);
        }

        private AssignmentViewModel.Assignment GetApplicationUserAssignment(int id)
        {
            return _repository.GetAssignment(id).Select(a => new AssignmentViewModel.Assignment
            {
                Id = a.Id,
                Title = a.Title,
                Question = a.Question,
                XPReward = a.XPReward,
                Answers = a.Answers.Select(a => new AssignmentViewModel.Answer { 
                    Id = a.Id, 
                    IsCorrect = a.IsCorrect, 
                    Text = a.Text}).ToList()
            }).SingleOrDefault();
        }
    }
}