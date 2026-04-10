namespace ELearning.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Course>(builder =>
            {
                builder.HasOne(x => x.Grade)
                    .WithMany(x => x.Courses)
                    .HasForeignKey(x => x.GradeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Module>(builder =>
            {
                builder.HasOne(x => x.Course)
                    .WithMany(x => x.Modules)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Lesson>(builder =>
            {
                builder.HasOne(x => x.Module)
                    .WithMany(x => x.Lessons)
                    .HasForeignKey(x => x.ModuleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Content>(builder =>
            {
                builder.HasOne(x => x.Lesson)
                    .WithMany(x => x.Contents)
                    .HasForeignKey(x => x.LessonId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Enrollment>(builder =>
            {
                builder.HasOne(x => x.Course)
                    .WithOne(x => x.Enrollment)
                    .HasForeignKey<Enrollment>(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(x => x.User)
                   .WithOne(x => x.Enrollment)
                   .HasForeignKey<Enrollment>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
