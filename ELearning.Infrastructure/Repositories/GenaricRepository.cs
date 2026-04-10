namespace ELearning.Infrastructure.Repositories
{
    public class GenaricRepository<T>(AppDbContext context) : IGenaricRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context;
        internal DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<T> Update(T entity)
        {
            //throw new NotImplementedException();

            _dbSet.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            return true;
        }
    }
}
