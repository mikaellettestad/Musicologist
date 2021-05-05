using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System.Linq;

namespace Musicologist.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Course> GetCourses()
        {
            return _context.Courses;
        }

        public IQueryable<Course> GetCourseOverview(int Id)
        {
            return _context.Courses.Where(c => c.Id == Id);
        }

        public IQueryable<Course> GetCourse(int courseId)
        {
            return _context.Courses.Where(course => course.Id == courseId)
                .Include(course => course.CourseParts)
                .ThenInclude(course => course.Lessons);
        }
    }
}