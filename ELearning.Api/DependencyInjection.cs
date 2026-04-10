namespace ELearning.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                    .AddInfrastructureDIServer(configuration)
                    .AddCoreDI();

            return services;
        }
    }
}
