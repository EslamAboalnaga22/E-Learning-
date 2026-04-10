namespace ELearning.Infrastructure.Repositories
{
    public class GradeRepository(AppDbContext context) : GenaricRepository<Grade>(context), IGradeRepository
    {

    }
}
