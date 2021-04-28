namespace Musicologist.Models
{
    public class ApplicationUserCourse
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Course Course { get; set; }
        public bool IsCompleted { get; set; }
        public int XPEarned { get; set; }
        public int AssignmentsCompleted { get; set; }
    }
}