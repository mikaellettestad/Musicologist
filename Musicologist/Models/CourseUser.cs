namespace Musicologist.Models
{
    public class CourseUser
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Course Course { get; set; }
    }
}