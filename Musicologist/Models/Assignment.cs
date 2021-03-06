using System.Collections.Generic;

namespace Musicologist.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public bool IsCompleted { get; set; }
        public List<Answer> Answers { get; set; }
    }
}