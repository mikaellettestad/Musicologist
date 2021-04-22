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

        // Så här ser modellen ut
        // Tabellen binder samman användare och kurs
        // Kanske behövs en variabel: int XPEarned? Hmmmmm

        //public class ApplicationUserCourse
        //{
        //    public int Id { get; set; }
        //    public ApplicationUser ApplicationUser { get; set; }
        //    public Course Course { get; set; }
        //    public bool IsCompleted { get; set; }
        //}

        // H
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