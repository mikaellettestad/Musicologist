using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IAssignmentRepository : IApplicationUserCourseRepository
    {
        IQueryable<Assignment> GetAssignment(int assignmentid);
        //void UpdateApplicationUserCourse(string applicationUserId, int courseId, int xpEarned);
        void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted);
    }
}
