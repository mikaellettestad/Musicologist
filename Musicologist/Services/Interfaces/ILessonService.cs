namespace Musicologist.Services.Interfaces
{
    public interface ILessonService
    {
        int GetNextLessonId(int courseId, int i);
    }
}
