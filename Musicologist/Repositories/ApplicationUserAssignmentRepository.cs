using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Repositories
{
    public class ApplicationUserAssignmentRepository : IApplicationUserAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            var applicationUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == applicationUserId);

            var assignment = GetAssignment(assignmentId).SingleOrDefault();

            var model = new ApplicationUserAssignment()
            {
                ApplicationUser = applicationUser,
                Assignment = assignment,
                IsCompleted = isCompleted
            };

            _context.ApplicationUserAssignments.Add(model);

            _context.SaveChanges();
        }

        public IQueryable<Assignment> GetAssignment(int assignmentId)
        {
            return _context.Assignments.Where(a => a.Id == assignmentId).Include(a => a.Answers);
        }
    }
}
