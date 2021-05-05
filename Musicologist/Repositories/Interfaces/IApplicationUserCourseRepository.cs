using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserCourseRepository : IApplicationUserRepository
    {
        IQueryable<ApplicationUserCourse> GetCourse(string applicationUserId, int courseId);
        IQueryable<ApplicationUserCourse> GetCourseDetails(string applicationUserId, int courseId);
        IQueryable<ApplicationUserAssignment> GetAssignment(string applicationUserId, int assignmentId);
        void AddApplicationUserCourse(string applicationUserId, int courseId);
        void UpdateApplicationUserCourse(string applicationUserId, int courseId, int XPEarned, int assignmentsCompleted);
    }
}