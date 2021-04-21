using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Models
{
    public class AssignmentUser
    {
        public int Id { get; set; }
        public Assignment Assignment { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsCompleted { get; set; }
    }
}
