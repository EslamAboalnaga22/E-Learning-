using CurrieTechnologies.Razor.SweetAlert2;
using ELearning.Application;
using ELearning.Core;
using ELearning.Core.InterfacesClient;
using ELearning.Infrastructure;

namespace ELearningWASM
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWsamDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDIClient(configuration)
                .AddCoreDI();

            services.AddSweetAlert2();

            return services;
        }
    }
}
