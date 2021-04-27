using System.Collections.Generic;

namespace Musicologist.ViewModels
{
    public class CourseViewModel
    {
        public Course CurrentCourse { get; set; }
        public List<Course> Courses { get; set; }
        public class Course
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int XPReward { get; set; }
            public string ImageUrl { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}