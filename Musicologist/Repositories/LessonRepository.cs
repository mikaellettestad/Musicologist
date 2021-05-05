using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Musicologist.Repositories
{
    public class LessonRepository : ApplicationUserCourseRepository, ILessonRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Lesson> GetLesson(int lessonId)
        {
            return _context.Lessons.Where(lesson => lesson.Id == lessonId)
                .Include(lesson => lesson.LessonTexts)
                .Include(lesson => lesson.LessonImages)
                .Include(lesson => lesson.Assignment);
        }

        public List<Lesson> GetLessons(int courseId)
        {
            var course = _context.Courses.Where(course => course.Id == courseId)
                .Include(course => course.CourseParts)
                .ThenInclude(course => course.Lessons).SingleOrDefault();

            var lessons = new List<Lesson>();

            foreach (var coursePart in course.CourseParts)
            {
                foreach (var lesson in coursePart.Lessons)
                {
                    lessons.Add(lesson);
                }
            }

            return lessons;
        }
    }
}