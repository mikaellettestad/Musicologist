using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface ILessonRepository : IApplicationUserCourseRepository
    {
        IQueryable<Lesson> GetLesson(int lessonId);
    }
}
