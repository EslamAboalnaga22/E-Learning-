namespace ELearning.Infrastructure.Repositories
{
    public class LessonRepository(AppDbContext context) : GenaricRepository<Lesson>(context), ILessonRepository
    {
        public override async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _dbSet.Include(x => x.Module)
                               .Include(x => x.Contents)
                               .ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonByModuleTitle(string moduleTitle)
        {
            return await _dbSet.Include(x => x.Module)
                   .Include(x => x.Contents)
                   .Where(x => x.Module!.Title == moduleTitle)
                   .ToListAsync();
        }

        public override async Task<Lesson> GetById(int id)
        {
            return await _dbSet.Include(x => x.Module)
                               .Include(x => x.Contents)
                               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
