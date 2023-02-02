using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        public IActionResult AddCourse()
        {
            return View();
        }
    }
}
