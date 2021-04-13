using System.Collections.Generic;

namespace Musicologist.ViewModels
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser CurrentApplicationUser{ get; set; }

        public string ConfirmationMessage { get; set; }
        public string ErrorMessage { get; set; }

        public class ApplicationUser
        {
            public string Email { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public List<Course> Courses { get; set; }
            public UserStatistics UserStatistics { get; set; }
        }
        public class Course
        {
            public string Title { get; set; }
        }

        public class UserStatistics
        {
            public int XPGainedTotal { get; set; }
        }
    }
}