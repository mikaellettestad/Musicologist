namespace Musicologist.Services.Interfaces
{
    public interface IAssignmentService
    {
        void AddResults(string applicationUserId, int courseId, int assignmentId, bool isCompleted);
    }
}
