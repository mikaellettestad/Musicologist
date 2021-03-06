using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Models
{
    public class UserStatistics
    {
        public int Id { get; set; }
        public int XPGainedTotal { get; set; }
    }
}