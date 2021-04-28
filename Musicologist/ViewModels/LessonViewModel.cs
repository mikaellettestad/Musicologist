using System.Collections.Generic;

namespace Musicologist.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LessonText> Texts { get; set; }
        public List<LessonImage> Images { get; set; }
        public int CourseId { get; set; }
        public int AssignmentId { get; set; }
        public bool IsCompleted { get; set; }
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
    }
}