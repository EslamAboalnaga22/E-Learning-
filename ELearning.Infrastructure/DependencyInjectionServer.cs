namespace ELearning.Infrastructure
{
    public static class DependencyInjectionServer
    {
        public static IServiceCollection AddInfrastructureDIServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            // JWT Configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // Forget Password & Reset Password
            //services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(2));

            services.AddCors(option =>
            {
                option.AddPolicy("blazor", opt =>
                {
                    opt.AllowAnyMethod();
                    opt.AllowAnyHeader();
                    opt.WithOrigins("https://localhost:7233");
                    opt.WithOrigins("http://localhost:5122");
                    opt.AllowCredentials();
                });
            });

            services.AddScoped<IUnitOfWrok, UnitOfWork>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddExceptionHandler<GlobalErrorHandlling>();
            services.AddProblemDetails();

            return services;
        }
    }
}
