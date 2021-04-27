using Musicologist.Repositories.Interfaces;
using Musicologist.Services.Interfaces;

namespace Musicologist.Services
{
    public class ApplicationUserAssignmentService : IApplicationUserAssignmentService
    {
        private readonly IAssignmentRepository _repository;

        public ApplicationUserAssignmentService(IAssignmentRepository repository)
        {
            _repository = repository;
        }

        public void Process(string applicationUserId, int courseId, int assignmentId, bool isCorrect, bool isCompleted)
        {

            //if (isCorrect)
            //{
            //    UpdateResults(applicationUserId, courseId, assignmentId, true);

            //    Model.AnswerIsCorrect = true;
            //    Model.AnswerIsIncorrect = false;
            //}
            //else
            //{
            //    UpdateResults(_userManager.GetUserId(User), model.CurrentCourseId, model.CurrentAssignment.Id, false);

            //    Model.AnswerIsCorrect = false;
            //    Model.AnswerIsIncorrect = true;
            //}


            throw new System.NotImplementedException();
        }

        //public Assignment Process(string applicationUserId, int assignmentId, bool isCompleted)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}