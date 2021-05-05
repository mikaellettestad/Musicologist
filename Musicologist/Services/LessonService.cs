using Musicologist.Repositories.Interfaces;
using Musicologist.Services.Interfaces;

namespace Musicologist.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _repository;

        public LessonService(ILessonRepository repository)
        {
            _repository = repository;
        }
        public int GetNextLessonId(int courseId, int i)
        {
            var lessons = _repository.GetLessons(courseId);
            
            if((i + 1) < lessons.Count)
            {
                return lessons[i + 1].Id;
            }

            return 0;
        }
    }
}

