using Microsoft.AspNetCore.Mvc;
using Moq;
using Musicologist.Controllers;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Musicologist.Tests.Controllers
{
    public class TestApplicationControllerFacts
    {
        public Mock<IApplicationRepository> _repository { get; set; }
        public ApplicationController _sut { get; set; }
        public TestApplicationControllerFacts()
        {
            _repository = new Mock<IApplicationRepository>();

            _sut = new ApplicationController(_repository.Object);
        }

        [Fact]
        public void Index_Returns_Correct_Action_Result()
        {
            var expected = typeof(ViewResult);

            var actual = _sut.Index();

            Assert.IsType(expected, actual);
        }

        [Fact]
        public void Get_Courses_Returns_Correct_Data_Type()
        {
            var expected = typeof(List<CourseViewModel.Course>);

            var actual = _sut.GetCourses();

            Assert.IsType(expected, actual);
        }

        [Fact]
        public void Get_Details_Returns_Course_View_Model_If_Course_Does_Exist()
        {
            var courses = new List<Course> { new Course() { Title = "Music Theory Basics" } };
            
            _repository.Setup(r => r.GetCourseOverview(1)).Returns(courses.AsQueryable());

            var expected = typeof(CourseViewModel);

            var actual = _sut.GetDetails(1);

            Assert.IsType(expected, actual);
        }

        [Fact]
        public void Get_Details_Returns_Null_If_Course_Does_Not_Exist()
        {
            int existingCourse = 1;

            int nonExistingCourse = 2;

            var courses = new List<Course> { new Course() { Title = "Music Theory Basics" } };

            _repository.Setup(r => r.GetCourseOverview(existingCourse)).Returns(courses.AsQueryable());

            Assert.Null(_sut.GetDetails(nonExistingCourse));
        }
    }
}