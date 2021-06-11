using Microsoft.AspNetCore.Mvc;
using Musicologist.Repositories.Interfaces;

namespace Musicologist.Controllers
{
    public class HomeController : CourseController
    {
        private readonly IApplicationRepository _repository;
        public HomeController(IApplicationRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}