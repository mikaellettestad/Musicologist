using System.Collections.Generic;

namespace Musicologist.ViewModels
{
    public class CourseViewModel
    {
        public Course CurrentCourse { get; set; }
        public List<Course> Courses { get; set; }
        public Lesson CurrentLesson { get; set; }
        public class Course
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int XP { get; set; }
            public string ImageUrl { get; set; }
            public bool IsCompleted { get; set; }
            public List<CoursePart> CourseParts { get; set; }
        }

        public class CoursePart
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public List<Lesson> Lessons { get; set; }
        }

        public class Lesson
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public List<LessonText> LessonTexts { get; set; }
            public List<LessonImage> LessonImages { get; set; }
            public Assignment Assignment { get; set; }
        }

        public class LessonText
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
        }

        public class LessonImage
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
        }

        public class Assignment
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Question { get; set; }
            public bool IsCompleted { get; set; }
            public List<Answer> Answers { get; set; }
        }

        public class Answer
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}