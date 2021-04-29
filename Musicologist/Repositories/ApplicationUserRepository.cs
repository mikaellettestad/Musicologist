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

        public IQueryable<ApplicationUser> GetApplicationUser(string applicationUserId)
        {
            return _context.ApplicationUsers.Where(x => x.Id == applicationUserId);
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

        public void UpdateApplicationUser(string applicationUserId, int xp)
        {
            var applicationUser = GetApplicationUser(applicationUserId).SingleOrDefault();

            applicationUser.XP = xp;

            _context.ApplicationUsers.Update(applicationUser);

            _context.SaveChanges();
        }
    }
}