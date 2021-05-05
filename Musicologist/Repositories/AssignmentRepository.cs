using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System.Linq;

namespace Musicologist.Repositories
{
    public class AssignmentRepository : ApplicationUserCourseRepository, IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //Service-klass
        public void UpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            var model = GetAssignment(applicationUserId, assignmentId).SingleOrDefault();

            model.IsCompleted = isCompleted;

            _context.ApplicationUserAssignments.Update(model);

            _context.SaveChanges();
        }

        public IQueryable<Assignment> GetAssignment(int assignmentId)
        {
            return _context.Assignments.Where(a => a.Id == assignmentId).Include(a => a.Answers);
        }

        //Service-klass
        public void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            //var applicationUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == applicationUserId);

            var applicationUser = GetApplicationUser(applicationUserId).SingleOrDefault();

            //var assignment = _context.Assignments.SingleOrDefault(a => a.Id == assignmentId);

            var assignment = GetAssignment(assignmentId).SingleOrDefault();

            var applicationUserAssignment = new ApplicationUserAssignment()
            {
                ApplicationUser = applicationUser,
                Assignment = assignment,
                IsCompleted = isCompleted
            };

            _context.Add(applicationUserAssignment);

            _context.SaveChanges();
        }
    }
}