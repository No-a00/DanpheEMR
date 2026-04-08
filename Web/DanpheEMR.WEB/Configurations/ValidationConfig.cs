using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace DanpheEMR.WEB.Configurations
{
    public static class ValidationConfig
    {
        public static IServiceCollection AddValidationConfiguration(this IServiceCollection services)
        {
            var applicationAssembly = typeof(Result).Assembly;

            services.AddValidatorsFromAssembly(applicationAssembly);

            return services;
        }
    }
}