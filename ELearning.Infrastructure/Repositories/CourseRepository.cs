namespace ELearning.Infrastructure.Repositories
{
    public class CourseRepository(AppDbContext context) : GenaricRepository<Course>(context), ICourseRepository
    {
        public override async Task<IEnumerable<Course>> GetAll()
        {
            return await _dbSet.Include(x => x.Grade).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllByGradeId(int gradeId)
        {
            return await _dbSet.Include(x => x.Grade).Where(x=> x.GradeId == gradeId).ToListAsync();
        }

        public override async Task<Course> GetById(int id)
        {
            return await _dbSet.Include(x => x.Grade).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
