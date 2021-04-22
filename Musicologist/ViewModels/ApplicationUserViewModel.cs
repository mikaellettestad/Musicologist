using System.Collections.Generic;

namespace Musicologist.ViewModels
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser CurrentApplicationUser{ get; set; }

        public string ConfirmationMessage { get; set; }
        public string ErrorMessage { get; set; }
        public ApplicationUserCourse CurrentCourse { get; set; }

        public class ApplicationUser
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }

            // Detta ska tracka mig som användare
            public List<ApplicationUserCourse> ApplicationUserCourses { get; set; }
            public UserStatistics UserStatistics { get; set; }
        }

        public class ApplicationUserCourse
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool IsCompleted { get; set; }
        }

        public class UserStatistics
        {
            public int XPGainedTotal { get; set; }
        }
    }
}