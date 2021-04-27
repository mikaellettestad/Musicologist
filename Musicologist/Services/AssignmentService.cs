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

        public void UpdateResults(string applicationUserId, int courseId, int assignmentId, bool isCompleted)
        {
            var assignment = _repository.GetAssignment(assignmentId).SingleOrDefault();

            if (isCompleted)
            {
                var applicationUserCourse = _repository.GetApplicationUserCourse(applicationUserId, courseId).SingleOrDefault();

                applicationUserCourse.XPEarned += assignment.XPReward;

                _repository.UpdateApplicationUserCourse(applicationUserId, courseId, applicationUserCourse.XPEarned);

                AddOrUpdateApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
            else
            {
                AddOrUpdateApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
        }

        public void AddOrUpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            var model = _repository.GetApplicationUserAssignment(applicationUserId, assignmentId).SingleOrDefault();

            if (model != null)
            {
                _repository.UpdateApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
            else
            {
                _repository.AddApplicationUserAssignment(applicationUserId, assignmentId, isCompleted);
            }
        }
    }
}