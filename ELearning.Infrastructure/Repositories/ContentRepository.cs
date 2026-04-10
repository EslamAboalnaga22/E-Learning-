using ELearning.Core.Entities;

namespace ELearning.Infrastructure.Repositories
{
    public class ContentRepository(AppDbContext context, IWebHostEnvironment environment) : GenaricRepository<Content>(context), IContentRepository
    {
        private readonly string FullPath = $"{environment.WebRootPath}/PDF";

        public override async Task<IEnumerable<Content>> GetAll()
        {
            return await _dbSet.Include(x => x.Lesson).ToListAsync();
        }

        public override async Task<Content> GetById(int id)
        {
            return await _dbSet.Include(x => x.Lesson).FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task<Content> Update(Content content)
        {
            var mainContent = await GetById(content.Id);

            if (mainContent is null)
                return null;

            mainContent.Title = content.Title;
            mainContent.LessonId = content.LessonId;

            if (!string.IsNullOrEmpty(content.FilePath))
            {
                mainContent.Type = content.Type;
                mainContent.FilePath = content.FilePath;
            }
                
            return mainContent;
        }
        public async Task<string> SaveFile(byte[] file, string FilePath)
        {

            var FileName = $"{Guid.NewGuid()}_{FilePath}";

            var path = Path.Combine(FullPath, FileName);

            await using var stream = new FileStream(path, FileMode.Create);

            stream.Write(file, 0, file.Length);

            return FileName;
        }

        public bool UpdateFile(bool effected, bool hasNewFile, string oldFile, string FilePath)
        {
            if (effected)
            {
                if (hasNewFile)
                {
                    var file = Path.Combine(FullPath, oldFile);
                    File.Delete(file);
                }

                return true;
            }
            else
            {
                var file = Path.Combine(FullPath, FilePath);
                File.Delete(file);
                return false;
            }
        }

        public bool DeleteFile(bool effected, string FilePath)
        {
            var cover = Path.Combine(FullPath, FilePath);
            File.Delete(cover);
            return true;
        }


        //public async Task<string> SaveFile(IFormFile file)
        //{
        //    var FileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        //    var path = Path.Combine(FullPath, FileName);

        //    using var stream = File.Create(path);
        //    await file.CopyToAsync(stream);

        //    return FileName;
        //}
    }
}
