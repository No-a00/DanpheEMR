using DanpheEMR.Api.Services;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface.Auth;
using DanpheEMR.Core.Interfaces.Base;
using DanpheEMR.DataAccess.Repositories.Patients;
using DanpheEMR.Infrastructure.Data;
using DanpheEMR.WEB.Authentication;
using DanpheEMR.WEB.Configurations;

namespace DanpheEMR.WEB.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapperConfiguration();
            services.AddMediatRConfiguration();
            services.AddValidationConfiguration();

            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtProvider, JwtProvider>();

         
            services.AddScoped(typeof(DanpheEMR.Core.Interface.Base.IGenericRepository<>),
                               typeof(DanpheEMR.DataAccess.Repositories.Base.GenericRepository<>));

            services.AddScoped<DanpheEMR.Application.Abstractions.Services.Admin.IAuthService,
                               DanpheEMR.DataAccess.Services.Admin.AuthService>();

            var repositoryAssembly = typeof(PatientRepository).Assembly;
            var repositoryTypes = repositoryAssembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"));

            foreach (var repoType in repositoryTypes)
            {
                var interfaceType = repoType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repoType.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, repoType);
                }
            }

            return services;
        }
    }
}