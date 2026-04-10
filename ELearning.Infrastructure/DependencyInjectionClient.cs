using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace ELearning.Infrastructure
{
    public static class DependencyInjectionClient
    {
        public static IServiceCollection AddInfrastructureDIClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7185/")
            });

            services.AddScoped<IMainClientRepository<GradeDto>, MainClientRepository<GradeDto>>();

            services.AddScoped<IMainClientRepository<CourseDtoDetails>, MainClientRepository<CourseDtoDetails>>();
            services.AddScoped<IMainClientRepository<CourseDtoRequest>, MainClientRepository<CourseDtoRequest>>();

            services.AddScoped<IMainClientRepository<ModuleDtoDetails>, MainClientRepository<ModuleDtoDetails>>();
            services.AddScoped<IMainClientRepository<ModuleDtoRequest>, MainClientRepository<ModuleDtoRequest>>();

            services.AddScoped<IMainClientRepository<LessonDtoDetails>, MainClientRepository<LessonDtoDetails>>();
            services.AddScoped<IMainClientRepository<LessonDtoRequest>, MainClientRepository<LessonDtoRequest>>();

            services.AddScoped<IMainClientRepository<ContentDtoDetails>, MainClientRepository<ContentDtoDetails>>();
            services.AddScoped<IMainClientRepository<ContentCreateDtoRequest>, MainClientRepository<ContentCreateDtoRequest>>();
            services.AddScoped<IMainClientRepository<ContentUpdateDtoRequest>, MainClientRepository<ContentUpdateDtoRequest>>();

            services.AddScoped<IMainClientRepository<RoleModel>, MainClientRepository<RoleModel>>();

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            // Local Storage
            services.AddBlazoredLocalStorage();

            // Authentication State Provider
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, AppAuthenticatoinStateProvider>();

            return services;
        }
    }
}
