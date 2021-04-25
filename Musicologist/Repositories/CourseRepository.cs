using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Course> GetAllCourseDetails()
        {
            return _context.Courses;
        }

        public IQueryable<Course> GetCourseDetails(int courseId)
        {
            return _context.Courses.Where(c => c.Id == courseId);
        }

        public IQueryable<Course> GetCourse(int Id)
        {
            return _context.Courses.Where(c => c.Id == Id)
                .Include(c => c.CourseParts)
                .ThenInclude(c => c.Lessons)
                .ThenInclude(c => c.Assignment);
        }

        public IQueryable<ApplicationUserAssignment> GetApplicationUserAssignments(string applicationUserId, int assignmentId)
        {
            return _context.ApplicationUserAssignments
                .Where(a => a.ApplicationUser.Id == applicationUserId && a.Assignment.Id == assignmentId);
        }

        public IQueryable<ApplicationUserCourse> GetApplicationUserCourses(string applicationUserId)
        {
            return _context.ApplicationUserCourses.Where(a => a.ApplicationUser.Id == applicationUserId)
                .Include(a => a.Course);
        }

        public IQueryable<Lesson> GetLesson(int Id)
        {
            return _context.Lessons.Where(l => l.Id == Id)
                .Include(l => l.LessonTexts)
                .Include(l => l.LessonImages)
                .Include(l => l.Assignment)
                .ThenInclude(l => l.Answers);
        }
    }
}
