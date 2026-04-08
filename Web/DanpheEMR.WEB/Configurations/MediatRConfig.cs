using Microsoft.Extensions.DependencyInjection;

namespace DanpheEMR.WEB.Configurations
{
    public static class MediatRConfig
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            var applicationAssembly = typeof(Result).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            return services;
        }
    }
}