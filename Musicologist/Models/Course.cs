using System.Collections.Generic;

namespace Musicologist.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public bool IsCompleted { get; set; }
        public List<CoursePart> CourseParts { get; set; }
    }
}