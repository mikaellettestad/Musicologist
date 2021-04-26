﻿using Musicologist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserAssignmentRepository
    {
        IQueryable<Assignment> GetAssignment(int assignmentid);
        void UpdateApplicationUserAssignment(string applicationUserId, int assignmentId, bool IsCompleted);
        void AddApplicationUserAssignment(string applicationUserId, int assignmentId, bool IsCompleted);
        IQueryable<ApplicationUserAssignment> GetApplicationUserAssignment(string applicationUserId, int assignmentId);
    }
}
