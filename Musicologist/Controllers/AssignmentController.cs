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

        public IActionResult Index(int assignmentId, int courseId)
        {
            Model.CurrentAssignment = GetAssignment(assignmentId);

            Model.CurrentCourseId = courseId;
             
            Model.AnswerIsCorrect = false;

            Model.AnswerIsIncorrect = false;

            return View(Model);
        }

        //Service-klass
        [HttpPost]
        public IActionResult Index(AssignmentViewModel model)
        {
            Model.CurrentAssignment = GetAssignment(model.CurrentAssignment.Id);

            Model.CurrentCourseId = model.CurrentCourseId;

            //if (!model.CurrentAnswer.IsCorrect)
            //{
            //    Model.AnswerIsCorrect = false;

            //    Model.AnswerIsIncorrect = true;

            //    return View(Model);
            //}

            if (model.CurrentAnswer.IsCorrect)
            {
                _service.UpdateResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id, true);

                Model.AnswerIsCorrect = true;
                Model.AnswerIsIncorrect = false;
            }
            else
            {
                _service.UpdateResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id, false);

                Model.AnswerIsCorrect = false;
                Model.AnswerIsIncorrect = true;
            }

            return View(Model);
        }

        private AssignmentViewModel.Assignment GetAssignment(int id)
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