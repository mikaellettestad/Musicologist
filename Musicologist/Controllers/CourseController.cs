using Microsoft.AspNetCore.Mvc;
using Musicologist.Repositories.Interfaces;

namespace Musicologist.Controllers
{
    public class CourseController : ApplicationController
    {
        private readonly IApplicationRepository _repository;

        public CourseController(IApplicationRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IActionResult Details(int courseId)
        {
            return View(GetDetails(courseId));
        }
    }
}