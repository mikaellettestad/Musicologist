using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Diagnostics;

namespace Musicologist.Controllers
{
    public class HomeController : CourseController
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly ICourseRepository _repository;
        public HomeController(ILogger<HomeController> logger, ICourseRepository repository) : base(repository)
        {
            _logger = logger;
            //_repository = repository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}