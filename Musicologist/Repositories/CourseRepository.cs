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

        public IQueryable<Course> GetCourseDetails(int Id)
        {
            return _context.Courses.Where(c => c.Id == Id);
        }

        public IQueryable<Course> GetCourse(int Id)
        {
            return _context.Courses.Where(c => c.Id == Id)
                .Include(c => c.CourseParts)
                .ThenInclude(c => c.Lessons);
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
