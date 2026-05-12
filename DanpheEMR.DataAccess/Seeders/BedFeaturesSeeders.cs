
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace DanpheEMR.DataAccess.Seeders
{
    public static class BedFeatureSeeder
    {

        public static async Task SeedDataAsync(ApplicationDbContext context)
        {

            if (!await context.BedFeatures.AnyAsync())
            {
                var bedFeature = new List<BedFeature>
                {
                        new BedFeature
                        {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        FeatureCode = "STD",
                        FeatureName = "Giường Thường",
                        Description = "Giường tiêu chuẩn nằm phòng chung",
                        BedPrice = 150000m,
                        IsDeleted = false
                        },
                        new BedFeature
                        {
                            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                            FeatureCode = "VIP",
                            FeatureName = "Giường VIP 1 người",
                            Description = "Giường bệnh phòng riêng biệt, đầy đủ tiện nghi cao cấp",
                            BedPrice = 800000m,
                            IsDeleted = false
                        },
                        new BedFeature
                        {
                            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                            FeatureCode = "ICU",
                            FeatureName = "Giường Hồi sức tích cực",
                            Description = "Giường trang bị máy thở và monitor theo dõi sinh tồn liên tục",
                            BedPrice = 1500000m,
                            IsDeleted = false
                        }

                };
                await context.BedFeatures.AddRangeAsync(bedFeature);
                await context.SaveChangesAsync();
            }

        }
    }
}