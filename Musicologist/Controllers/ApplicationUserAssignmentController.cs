using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
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

            Model.AnswerIsCorrect = false;

            Model.AnswerIsIncorrect = false;

            return View(Model);
        }

        [HttpPost]
        public IActionResult Index(AssignmentViewModel model)
        {
            Model.CurrentAssignment = GetAssignment(model.CurrentAssignment.Id);

            if (model.CurrentAnswer.IsCorrect)
            {
                UpdateResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id, true);

                Model.AnswerIsCorrect = true;
                Model.AnswerIsIncorrect = false;
            }
            else
            {
                UpdateResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id, false);

                Model.AnswerIsCorrect = false;
                Model.AnswerIsIncorrect = true;
            }

            Model.CurrentCourseId = model.CurrentCourseId;

            return View(Model);
        }

        private void UpdateResults(string applicationUserId, int courseId, int assignmentId, bool isCompleted)
        {
            var assignment = _assignmentRepository.GetAssignment(assignmentId).SingleOrDefault();

            if (isCompleted)
            {
                var applicationUserCourse = _applicationUserCourseRepository.GetApplicationUserCourse(applicationUserId, courseId).SingleOrDefault();

                applicationUserCourse.XPEarned += assignment.XPReward;

                _applicationUserCourseRepository.UpdateApplicationUserCourse(applicationUserId, courseId, applicationUserCourse.XPEarned);

                AddOrUpdateApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
            else
            {
                AddOrUpdateApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
        }

        private void AddOrUpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            var model =_assignmentRepository.GetApplicationUserAssignment(applicationUserId, assignmentId).SingleOrDefault();
            
            if(model != null)
            {
                _assignmentRepository.UpdateApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
            else
            {
                _assignmentRepository.AddApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
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