using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserCourseRepository : IApplicationUserRepository
    {
        IQueryable<ApplicationUserCourse> GetCourse(string applicationUserId, int courseId);
        public IQueryable<ApplicationUserAssignment> GetApplicationUserAssignment(string applicationUserId, int assignmentId);
        IQueryable<ApplicationUserCourse> GetApplicationUserCourse(string applicationUserId, int courseId);
        //IQueryable<ApplicationUserCourse> GetApplicationUserCourses(string applicationUserId);
        IQueryable<Course> GetCourseDetails(int courseId);
        void AddApplicationUserCourse(string applicationUserId, int courseId);
        public void UpdateApplicationUserCourse(string applicationUserId, int courseId, int XPEarned);
        IQueryable<Course> GetCourse(int Id);
    }
}