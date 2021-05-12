using Microsoft.AspNetCore.Mvc;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;

namespace Musicologist.Controllers
{
    public class CourseController : ApplicationController
    {
        private readonly IApplicationRepository _repository;

        public CourseController(IApplicationRepository repository) : base(repository)
        {
            _repository = repository;

            Model = new CourseViewModel();
        }

        public IActionResult Details(int courseId)
        {
            Model = GetDetails(courseId);

            if (Model != null) { return View(Model); }

            return new StatusCodeResult(404);
        }
    }
}