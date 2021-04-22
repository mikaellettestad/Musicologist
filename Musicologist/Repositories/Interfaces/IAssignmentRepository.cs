﻿using Musicologist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Repositories.Interfaces
{
    public interface IAssignmentRepository
    {
        IQueryable<Assignment> GetAssignment(int id);
    }
}
