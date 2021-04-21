using System.Collections.Generic;

namespace Musicologist.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public int XPRewardIfCompleted { get; set; }
        public List<Answer> Answers { get; set; }
    }
}