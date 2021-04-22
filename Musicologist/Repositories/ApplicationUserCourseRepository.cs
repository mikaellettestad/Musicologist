﻿using Microsoft.EntityFrameworkCore;
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
    public class ApplicationUserCourseRepository : IApplicationUserCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ApplicationUserCourse> GetCourse(string applicationUserId, int courseId)
        {
            return _context.ApplicationUserCourses.Where(c => c.ApplicationUser.Id == applicationUserId &&  c.Course.Id == courseId)
                .Include(c => c.Course)
                .ThenInclude(c => c.CourseParts)
                .ThenInclude(c => c.Lessons)
                .ThenInclude(c => c.Assignment);
        }

        public IQueryable<ApplicationUserAssignment> GetAssignment(string applicationUserId, int assignmentId)
        {
            return _context.ApplicationUserAssignments.Where(a => a.ApplicationUser.Id == applicationUserId && a.Assignment.Id == assignmentId);
        }
    }
}