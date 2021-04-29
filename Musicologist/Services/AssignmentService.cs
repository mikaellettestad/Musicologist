using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services.Interfaces;
using System.Linq;

namespace Musicologist.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repository;

        public AssignmentService(IAssignmentRepository repository)
        {
            _repository = repository;
        }

        public void AddResults(string applicationUserId, int courseId, int assignmentId, bool isCompleted)
        {
            AddApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);

            UpdateApplicationUserCourse(applicationUserId, courseId, assignmentId);

            UpdateApplicationUser(applicationUserId, assignmentId);
        }
        private void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            _repository.AddApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
        }

        private void UpdateApplicationUserCourse(string applicationUserId, int courseId, int assignmentId)
        {
            var applicationUserCourse = GetApplicationUserCourse(applicationUserId, courseId);

            int xpReward = GetXPReward(assignmentId);

            int xpEarned = applicationUserCourse.XPEarned += xpReward;

            int assignmentsCompleted = applicationUserCourse.AssignmentsCompleted += 1;

            _repository.UpdateApplicationUserCourse(applicationUserId, courseId, xpEarned, assignmentsCompleted);
        }

        private void UpdateApplicationUser(string applicationUserId, int assignmentId)
        {
            var applicationUser = GetApplicationUser(applicationUserId);

            int xpReward = GetXPReward(assignmentId);

            int xpEarned = applicationUser.XP += xpReward;

            _repository.UpdateApplicationUser(applicationUserId, xpEarned);
        }

        private ApplicationUser GetApplicationUser(string applicationUserId)
        {
            return _repository.GetApplicationUser(applicationUserId).SingleOrDefault();
        }

        private ApplicationUserCourse GetApplicationUserCourse(string applicationUserId, int courseId)
        {
            return _repository.GetApplicationUserCourse(applicationUserId, courseId).SingleOrDefault();
        }

        private int GetXPReward(int assignmentId)
        {
            return _repository.GetAssignment(assignmentId).SingleOrDefault().XPReward;
        }
    }
}