namespace ELearning.Infrastructure.Repositories
{
    public class ModuleRepository(AppDbContext context) : GenaricRepository<Module>(context), IModuleRepository
    {
        public override async Task<IEnumerable<Module>> GetAll()
        {
            return await _dbSet.Include(x => x.Course)
                               .ThenInclude(x => x.Grade)
                               .ToListAsync();
        }

        public async Task<IEnumerable<Module>> GetAllByCourseDescription(string descriptionCourse)
        {
            return await _dbSet.Include(x => x.Course)
                   .ThenInclude(x => x!.Grade)
                   .Where(x => x.Course!.Description == descriptionCourse)
                   .ToListAsync();
        }

        public override async Task<Module> GetById(int id)
        {
            return await _dbSet.Include(x => x.Course)
                               .ThenInclude(x => x.Grade)
                               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
