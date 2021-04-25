using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class ApplicationUserAssignmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserAssignmentRepository _assignmentRepository;
        private readonly IApplicationUserCourseRepository _applicationUserCourseRepository;
        public AssignmentViewModel Model;
        public ApplicationUserAssignmentController(UserManager<ApplicationUser> userManager, IApplicationUserAssignmentRepository assignmentRepository, IApplicationUserCourseRepository applicationUserCourseRepository)
        {
            _userManager = userManager;
            _assignmentRepository = assignmentRepository;
            _applicationUserCourseRepository = applicationUserCourseRepository;
            Model = new AssignmentViewModel();
        }

        public IActionResult Index(int assignmentId, int courseId)
        {
            Model.CurrentAssignment = GetAssignment(assignmentId);

            Model.CurrentCourseId = courseId;

            return View(Model);
        }

        [HttpPost]
        public IActionResult Index(AssignmentViewModel model)
        {
            Model.CurrentAssignment = GetAssignment(model.CurrentAssignment.Id);

            if (model.CurrentAnswer.IsCorrect)
            {

                AddResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id);

                return View(Model);
            }

            return View(Model);
        }

        private void AddResults(string applicationUserId, int courseId, int assignmentId)
        {
            var assignment = _assignmentRepository.GetAssignment(assignmentId).SingleOrDefault();

            var applicationUserCourse = _applicationUserCourseRepository.GetApplicationUserCourse(applicationUserId, courseId).SingleOrDefault();

            applicationUserCourse.XPEarned += assignment.XPReward;

            _assignmentRepository.AddApplicationUserAssignment(applicationUserId, assignmentId, true);
        }

        private AssignmentViewModel.Assignment GetAssignment(int id)
        {
            return _assignmentRepository.GetAssignment(id).Select(a => new AssignmentViewModel.Assignment
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