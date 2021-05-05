using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using System.Diagnostics;

namespace Musicologist.Controllers
{
    public class HomeController : CourseController
    {
        private readonly IGenericRepository _repository;
        public HomeController(IGenericRepository repository) : base(repository)
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}