namespace ELearning.Core.Interfaces
{
    public interface IUnitOfWrok : IDisposable
    {
        IGradeRepository Grades { get; }
        ICourseRepository Courses { get; }
        IModuleRepository Modules { get; }
        ILessonRepository Lessons { get; }
        IContentRepository Contents { get; }
        IUserRepository Users { get; }
        IAuthRepository Auths { get; }

        Task<bool> CompleteAsync();
    }
}
