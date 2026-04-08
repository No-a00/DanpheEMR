
namespace DanpheEMR.WEB.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
         
            var applicationAssembly = typeof(Result).Assembly;

            services.AddAutoMapper(applicationAssembly);

            return services;
        }
    }
}