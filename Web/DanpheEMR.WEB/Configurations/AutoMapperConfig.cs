namespace DanpheEMR.WEB.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => 
            {
                // AddMaps sẽ tự động quét tất cả các Profile có chung nhà (Assembly) với class này
                cfg.AddMaps(typeof(DanpheEMR.Application.Features.Patients.Commands.RegisterPatient.RegisterPatientMapping).Assembly);
            });

            return services;
        }
    }
}