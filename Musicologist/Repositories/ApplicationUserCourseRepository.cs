using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System.Linq;

namespace Musicologist.Repositories
{
    public class ApplicationUserCourseRepository : ApplicationUserRepository, IApplicationUserCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserCourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ApplicationUserCourse> GetCourse(string applicationUserId, int courseId)
        {
            return _context.ApplicationUserCourses.Where(c => c.ApplicationUser.Id == applicationUserId &&  c.Course.Id == courseId)
                .Include(c => c.Course)
                .ThenInclude(c => c.CourseParts)
                .ThenInclude(c => c.Lessons)
                .ThenInclude(c => c.Assignment);
        }

        public IQueryable<ApplicationUserCourse> GetApplicationUserCourse(string applicationUserId, int courseId)
        {
            return _context.ApplicationUserCourses.Where(c => c.ApplicationUser.Id == applicationUserId && c.Course.Id == courseId);
        }

        //Service-klass
        public void UpdateApplicationUserCourse(string applicationUserId, int courseId, int xpEarned, int assignmentsCompleted)
        {
            var applicationUserCourse = GetApplicationUserCourse(applicationUserId, courseId).SingleOrDefault();

            applicationUserCourse.XPEarned = xpEarned;

            applicationUserCourse.AssignmentsCompleted = assignmentsCompleted;

            _context.ApplicationUserCourses.Update(applicationUserCourse);

            _context.SaveChanges();
        }

        public IQueryable<ApplicationUserAssignment> GetApplicationUserAssignment(string applicationUserId, int assignmentId)
        {
            return _context.ApplicationUserAssignments.Where(a => a.ApplicationUser.Id == applicationUserId && a.Assignment.Id == assignmentId);
        }

        //Service-klass
        public void AddApplicationUserCourse(string applicationUserId, int courseId)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == courseId);

            var applicationUser = GetApplicationUser(applicationUserId).SingleOrDefault();

            var model = new ApplicationUserCourse() { 
                ApplicationUser = applicationUser,
                Course = course,
                IsCompleted = false };

            _context.ApplicationUserCourses.Add(model);

            _context.SaveChanges();
        }

        public IQueryable<Course> GetCourseDetails(int courseId)
        {
            return _context.Courses.Where(c => c.Id == courseId);
        }

        public IQueryable<Course> GetCourses()
        {
            return _context.Courses;
        }

        public IQueryable<Course> GetCourse(int Id)
        {
            return _context.Courses.Where(c => c.Id == Id);
        }
    }
}