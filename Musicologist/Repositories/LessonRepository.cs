using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
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
    }
}