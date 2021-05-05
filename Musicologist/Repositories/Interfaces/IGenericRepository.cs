using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IGenericRepository
    {
        IQueryable<Course> GetCourses();
        IQueryable<Course> GetCourseOverview(int Id);
        IQueryable<Course> GetCourse(int courseId);
    }
}