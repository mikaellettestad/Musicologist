using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Types;
using System.Linq;

namespace Musicologist.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ApplicationUser> GetUserProfile(string Id)
        {
            return _context.ApplicationUsers.Where(x => x.Id == Id)
                .Include(x => x.UserStatistics);
        }
        public IQueryable<ApplicationUserCourse> GetApplicationUserCourses(string Id)
        {
            return _context.ApplicationUserCourses.Where(c => c.ApplicationUser.Id == Id)
                .Include(c => c.Course);
        }

        public IQueryable<ApplicationUserAssignment> GetApplicationUserAssignments(string applicationUserId, int assignmentId)
        {
            return _context.ApplicationUserAssignments
                .Where(a => a.ApplicationUser.Id == applicationUserId && a.Assignment.Id == assignmentId);
        }

        public Update UpdateUser(string Id)
        {
            return Update.Succeeded;
        }
    }
}