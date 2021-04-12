using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Musicologist.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Course> Courses { get; set; }
        public UserStatistics UserStatistics { get; set; }
    }
}