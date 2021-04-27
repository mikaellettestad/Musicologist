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
    public class AssignmentRepository : ApplicationUserCourseRepository, IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            var model = GetApplicationUserAssignment(applicationUserId, assignmentId).SingleOrDefault();

            model.IsCompleted = isCompleted;

            _context.ApplicationUserAssignments.Update(model);

            _context.SaveChanges();
        }

        public IQueryable<Assignment> GetAssignment(int assignmentId)
        {
            return _context.Assignments.Where(a => a.Id == assignmentId).Include(a => a.Answers);
        }

        public void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted)
        {
            var applicationUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == applicationUserId);

            var assignment = _context.Assignments.SingleOrDefault(a => a.Id == assignmentId);

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