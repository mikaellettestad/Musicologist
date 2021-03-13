using Microsoft.AspNetCore.Mvc;
using Musicologist.Controllers;
using Xunit;

namespace Musicologist.Tests.Controllers
{
    public class TestControllerFacts
    {
        public TestController _sut { get; set; }

        public TestControllerFacts()
        {
            _sut = new TestController();
        }

        [Fact]
        public void Profile_Returns_ViewResult()
        {
            var expected = typeof(ViewResult);

            Assert.IsType(expected, _sut.Index());
        }
    }
}