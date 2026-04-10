namespace ELearning.Core.Interfaces
{
    public interface IContentRepository : IGenaricRepository<Content>
    {
        //Task<string> SaveFile(IFormFile file);
        Task<string> SaveFile(byte[] file, string FilePath);
        bool UpdateFile(bool effected, bool hasNewFile, string oldFile, string FilePath);
        bool DeleteFile(bool effected, string FilePath);
    }
}
