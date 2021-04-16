using System.Collections.Generic;

namespace Musicologist.Models
{
    public class CoursePart
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}