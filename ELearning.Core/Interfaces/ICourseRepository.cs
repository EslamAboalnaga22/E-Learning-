namespace ELearning.Core.Interfaces
{
    public interface ICourseRepository : IGenaricRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllByGradeId(int gradeId);
    }
}
