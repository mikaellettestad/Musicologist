using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Course> Courses { get; set; }
        public UserStatistics UserStatistics { get; set; }
    }
}
