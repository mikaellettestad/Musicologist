using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Musicologist.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int XP { get; set; }
    }
}