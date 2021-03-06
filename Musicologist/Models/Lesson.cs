using System.Collections.Generic;

namespace Musicologist.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LessonText> LessonTexts { get; set; }
        public List<LessonImage> LessonImages { get; set; }
        public Assignment Assignment { get; set; }
    }
}