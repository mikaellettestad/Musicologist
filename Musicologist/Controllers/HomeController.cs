using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories;
using Musicologist.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Musicologist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IUserRepository userRepository, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _userRepository = userRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var user = _context.ApplicationUsers.SingleOrDefault(x => x.Id == userId);

            if (user != null)
            {

                var userStatistics = new UserStatistics();

                userStatistics.XPGainedTotal = 890;

                user.UserStatistics = userStatistics;

                _context.Update(user);

                _context.SaveChanges();

            }
            
            return View();
        }

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
