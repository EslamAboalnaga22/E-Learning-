using ELearning.Core.InterfacesClient;

namespace ELearning.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWorkClient, UnitOfWorkClient>();


            return services;
        }
    }
}
