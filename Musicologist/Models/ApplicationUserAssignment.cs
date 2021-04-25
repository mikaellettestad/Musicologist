namespace Musicologist.Models
{
    public class ApplicationUserAssignment
    {
        public int Id { get; set; }
        public Assignment Assignment { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsCompleted { get; set; }
    }
}
