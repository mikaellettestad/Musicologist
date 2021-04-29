using Musicologist.Models;
using Musicologist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Services.Interfaces
{
    public interface IApplicationUserCourseService
    {
        int GetNumberOfLessons(ApplicationUserCourseViewModel.ApplicationUserCourse viewModel);
    }
}
