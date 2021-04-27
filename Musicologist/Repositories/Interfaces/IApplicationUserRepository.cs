using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        IQueryable<ApplicationUser> GetApplicationUser(string applicationUserId);
        IQueryable<ApplicationUserCourse> GetApplicationUserCourses(string Id);
    }
}
