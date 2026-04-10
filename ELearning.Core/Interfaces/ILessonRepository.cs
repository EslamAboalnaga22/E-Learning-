namespace ELearning.Core.Interfaces
{
    public interface ILessonRepository : IGenaricRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetAllLessonByModuleTitle(string moduleTitle);
    }
}
