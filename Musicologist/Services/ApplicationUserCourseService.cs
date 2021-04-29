using Musicologist.Services.Interfaces;
using Musicologist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Services
{
    public class ApplicationUserCourseService : IApplicationUserCourseService
    {
        public int GetNumberOfLessons(ApplicationUserCourseViewModel.ApplicationUserCourse viewModel)
        {
            int number = 0;

            foreach (var coursePart in viewModel.CourseParts)
            {
                number += coursePart.Lessons.Count();
            }

            return number;
        }
    }
}