using Musicologist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserCourseRepository
    {
        IQueryable<ApplicationUserCourse> GetCourse(string applicationUserId, int courseId);
        public IQueryable<ApplicationUserAssignment> GetAssignment(string applicationUserId, int assignmentId);
    }
}
