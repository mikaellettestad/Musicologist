using Microsoft.AspNetCore.Mvc;
using Musicologist.Controllers;
using Xunit;

namespace Musicologist.Tests.Controllers
{
    public class UserControllerFacts
    {
        public UserController _sut { get; set; }

        public UserControllerFacts()
        {
            //var Logger = new Mock<ILogger<HomeController>>();
            //var ApplicationDbContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            //var UserManager = new Mock<UserManager<ApplicationUser>>();
            //var UserRepository = new Mock<UserRepository>(ApplicationDbContext);

            //_sut = new HomeController(
            //    Logger.Object, 
            //    UserManager.Object, 
            //    UserRepository.Object, 
            //    ApplicationDbContext.Object);
            _sut = new UserController();
        }

        //[Fact]
        //public void Home_Returns_ViewResult()
        //{
        //    var expected = typeof(ViewResult);

        //    Assert.IsType(expected, _sut.Index());
        //}

        [Fact]
        public void Profile_Returns_ViewResult()
        {
            var expected = typeof(ViewResult);

            Assert.IsType(expected, _sut.Index());
        }
    }
}
