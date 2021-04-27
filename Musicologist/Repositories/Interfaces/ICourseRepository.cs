using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        IQueryable<Course> GetCourses();
        IQueryable<Course> GetCourse(int Id);
    }
}