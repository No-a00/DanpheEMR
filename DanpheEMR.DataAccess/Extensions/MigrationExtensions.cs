using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DanpheEMR.DataAccess.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task ApplyMigrationsAndSeed(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Tự động chạy Migration
            await context.Database.MigrateAsync();

            // Tự động chạy Seeder
            await RoleSeeder.SeedDataAsync(context);

            await BedFeatureSeeder.SeedDataAsync(context);
        }
    }
}
