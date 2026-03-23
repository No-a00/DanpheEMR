using DanpheEMR.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DanpheEMR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // 1. Dùng đúng cú pháp Action mà C# đang đòi hỏi:
            services.AddAutoMapper(cfg =>
            {
                // Bảo AutoMapper tự đi quét toàn bộ Profile trong Assembly này
                cfg.AddMaps(assembly);
            });

            // 2. FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            // 3. MediatR
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}