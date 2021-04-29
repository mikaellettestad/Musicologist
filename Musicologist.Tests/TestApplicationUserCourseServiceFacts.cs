using Musicologist.Services;
using Musicologist.Services.Interfaces;
using Musicologist.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace Musicologist.Tests
{
    public class TestApplicationUserCourseServiceFacts
    {
        public IApplicationUserCourseService _sut { get; set; }
        public ApplicationUserCourseViewModel Model { get; set; }
        public TestApplicationUserCourseServiceFacts()
        {
            _sut = new ApplicationUserCourseService();

            InitializeModel();
        }

        [Fact]
        public void Service_Returns_Correct_Number_Of_Lessons()
        {
            var expected = typeof(int);

            var actual = _sut.GetNumberOfLessons(Model.CurrentApplicationUserCourse);

            Assert.Equal(3, actual);
        }

        [Fact]
        public void Service_Returns_Correct_Data_Type()
        {
            var expected = typeof(int);

            var actual = _sut.GetNumberOfLessons(Model.CurrentApplicationUserCourse);

            Assert.IsType(expected, actual);
        }

        private void InitializeModel()
        {
            Model = new ApplicationUserCourseViewModel();

            Model.CurrentApplicationUserCourse = new ApplicationUserCourseViewModel.ApplicationUserCourse
            {
                CourseParts = new List<ApplicationUserCourseViewModel.CoursePart>
                {
                    new ApplicationUserCourseViewModel.CoursePart
                    {
                        Title = "First course part",
                        Lessons = new List<ApplicationUserCourseViewModel.Lesson>
                        {
                            new ApplicationUserCourseViewModel.Lesson
                            {
                                Title = "First lesson"
                            },
                            new ApplicationUserCourseViewModel.Lesson
                            {
                                Title = "Second lesson"
                            },
                            new ApplicationUserCourseViewModel.Lesson
                            {
                                Title = "Third lesson"
                            }
                        }
                    }
                }
            };
        }
    }
}