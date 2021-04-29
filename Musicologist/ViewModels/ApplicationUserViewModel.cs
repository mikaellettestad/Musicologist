using System.Collections.Generic;

namespace Musicologist.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public int XP { get; set; }

        public List<ApplicationUserCourse> ApplicationUserCourses { get; set; }
        public class ApplicationUserCourse
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}