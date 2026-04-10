namespace ELearning.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWrok
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

       private readonly IEmailSender _emailSender; 

        public IGradeRepository Grades { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IModuleRepository Modules { get; private set; }
        public ILessonRepository Lessons { get; private set; }
        public IContentRepository Contents { get; private set; }
        public IUserRepository Users { get; private set; }
        public IAuthRepository Auths { get; private set; }


        public UnitOfWork(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, IEmailSender emailSender)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _emailSender = emailSender;
            Grades = new GradeRepository(_context);
            Courses = new CourseRepository(_context);
            Modules = new ModuleRepository(_context);
            Lessons = new LessonRepository(_context);
            Contents = new ContentRepository(_context, _environment);
            Users = new UserRepository(_userManager, _roleManager);
            Auths = new AuthRepository(_userManager, _roleManager, _jwt, _emailSender);
            _emailSender = emailSender;
        }

        public async Task<bool> CompleteAsync() => await _context.SaveChangesAsync() > 0;

        public void Dispose() => _context.Dispose();
    }
}
