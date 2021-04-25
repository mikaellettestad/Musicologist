using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.ViewModels
{
    public class AssignmentViewModel
    {
        public Assignment CurrentAssignment { get; set; }
        public Answer CurrentAnswer { get; set; }
        public int CurrentCourseId { get; set; }
        public class Assignment
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Question { get; set; }
            public bool IsCompleted { get; set; }
            public int XPReward { get; set; }
            public List<Answer> Answers { get; set; }
        }

        public class Answer
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}
