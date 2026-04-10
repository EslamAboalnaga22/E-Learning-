namespace ELearning.Core.Interfaces
{
    public interface IModuleRepository : IGenaricRepository<Module>
    {
        Task<IEnumerable<Module>> GetAllByCourseDescription(string descriptionCourse);
    }
}
