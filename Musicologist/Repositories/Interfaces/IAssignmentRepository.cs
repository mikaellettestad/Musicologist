using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IAssignmentRepository : IApplicationUserCourseRepository
    {
        IQueryable<Assignment> GetAssignment(int assignmentid);
        void UpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool IsCompleted);
        void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool IsCompleted);
    }
}
