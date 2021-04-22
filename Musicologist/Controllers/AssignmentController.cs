using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssignmentRepository _assignmentRepository;
        public AssignmentViewModel Model;
        public AssignmentController(UserManager<ApplicationUser> userManager, IAssignmentRepository assignmentRepository)
        {
            _userManager = userManager;
            _assignmentRepository = assignmentRepository;
            Model = new AssignmentViewModel();
        }

        public IActionResult Assignment(int id)
        {
            Model.CurrentAssignment = GetAssignment(id);

            return View(Model);
        }

        [HttpPost]
        public IActionResult Assignment(AssignmentViewModel model)
        {
            Model.CurrentAssignment = GetAssignment(model.CurrentAssignment.Id);

            if (model.CurrentAnswer.IsCorrect)
            {
                var assignmentService = new AssignmentService();

                assignmentService.Register(_userManager.GetUserId(User), model.CurrentAssignment.Id);

                return View(Model);
            }

            return View(Model);
        }

        private AssignmentViewModel.Assignment GetAssignment(int id)
        {
            return _assignmentRepository.GetAssignment(id).Select(a => new AssignmentViewModel.Assignment
            {
                Id = a.Id,
                Title = a.Title,
                Question = a.Question,
                XPRewardIfCompleted = a.XPRewardIfCompleted,
                Answers = a.Answers.Select(a => new AssignmentViewModel.Answer { 
                    Id = a.Id, 
                    IsCorrect = a.IsCorrect, 
                    Text = a.Text}).ToList()
            }).SingleOrDefault();
        }
    }
}