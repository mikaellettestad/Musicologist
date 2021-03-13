using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Musicologist.Controllers;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories;
using Xunit;

namespace Musicologist.Tests.Controllers
{
    public class HomeControllerFacts
    {
        //public HomeController _sut { get; set; }

        //public HomeControllerFacts()
        //{
        //    var Logger = new Mock<ILogger<HomeController>>();
        //    var ApplicationDbContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            //var UserManager = new Mock<UserManager<ApplicationUser>>();
            //var UserRepository = new Mock<ApplicationUserRepository>(ApplicationDbContext.Object);

            //_sut = new HomeController(
                //Logger.Object,
                //UserManager.Object,
                //UserRepository.Object,
                //ApplicationDbContext.Object);
        //}

        //[Fact]
        //public void Index_Returns_ViewResult()
        //{
        //    var expected = typeof(ViewResult);

        //    Assert.IsType(expected, _sut.Index());
        //}
    }
}
