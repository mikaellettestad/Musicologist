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

        public IQueryable<ApplicationUser> GetUser(string Id)
        {
            return _context.ApplicationUsers.Where(x => x.Id == Id)
                .Include(x => x.Courses)
                .Include(x => x.UserStatistics);
        }

        public Update UpdateUser(string Id)
        {
            return Update.Succeeded;
        }
    }
}