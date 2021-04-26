using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        IQueryable<ApplicationUser> GetUserProfile(string Id);
        IQueryable<ApplicationUserCourse> GetApplicationUserCourses(string Id);
    }
}
