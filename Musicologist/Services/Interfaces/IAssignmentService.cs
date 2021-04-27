using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Services.Interfaces
{
    public interface IAssignmentService
    {
        void UpdateResults(string applicationUserId, int courseId, int assignmentId, bool isCompleted);
        void AddOrUpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool isCompleted);
    }
}
