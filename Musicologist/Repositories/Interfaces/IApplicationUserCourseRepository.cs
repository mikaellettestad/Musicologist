using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserCourseRepository
    {
        IQueryable<ApplicationUserCourse> GetCourse(string applicationUserId, int courseId);
        public IQueryable<ApplicationUserAssignment> GetAssignment(string applicationUserId, int assignmentId);
        IQueryable<ApplicationUserCourse> GetApplicationUserCourse(string applicationUserId, int courseId);
        IQueryable<Course> GetCourseDetails(int courseId);
        void AddApplicationUserCourse(string applicationUserId, int courseId);
    }
}