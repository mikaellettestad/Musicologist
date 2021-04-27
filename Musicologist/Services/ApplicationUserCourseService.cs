using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicologist.Services
{
    public class ApplicationUserCourseService
    {
        private readonly IApplicationUserCourseRepository _repository;

        public ApplicationUserCourseService(IApplicationUserCourseRepository repository)
        {
            _repository = repository;
        }


    }
}