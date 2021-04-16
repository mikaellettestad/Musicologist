using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        IQueryable<Course> GetAllCourseDetails();
        IQueryable<Course> GetCourseDetails(int Id);
        IQueryable<Course> GetCourse(int Id);
        IQueryable<Lesson> GetLesson(int Id);
    }
}