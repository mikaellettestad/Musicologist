using Musicologist.Models;
using System.Collections.Generic;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface ILessonRepository : IApplicationUserCourseRepository
    {
        IQueryable<Lesson> GetLesson(int lessonId);
        List<Lesson> GetLessons(int courseId);
    }
}
