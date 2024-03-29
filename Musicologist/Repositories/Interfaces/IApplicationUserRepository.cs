﻿using Musicologist.Models;
using System.Linq;

namespace Musicologist.Repositories.Interfaces
{
    public interface IApplicationUserRepository : IApplicationRepository
    {
        IQueryable<ApplicationUser> GetApplicationUser(string applicationUserId);
        IQueryable<ApplicationUserCourse> GetApplicationUserCourses(string Id);
        void UpdateApplicationUser(string applicationUserId, int xp);
    }
}
