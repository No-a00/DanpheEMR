using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface; // BẮT BUỘC: Namespace chứa 2 Interface (ISoftDelete, IHasActiveStatus)
using Microsoft.EntityFrameworkCore;
using System.Reflection; // BẮT BUỘC: Để sử dụng Reflection quét các bảng

namespace DanpheEMR.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // --- CÁC DBSET (BẢNG DATABASE) ---
        // Admin
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        // Patient & Clinical
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddresses { get; set; }
        public DbSet<PatientKin> PatientKins { get; set; }
        public DbSet<PatientGuarantee> PatientGuarantees { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        // Ward / Bed
        public DbSet<Ward> Wards { get; set; }
        public DbSet<BedFeature> BedFeatures { get; set; }
        public DbSet<Bed> Beds { get; set; }

        // OT (Phòng mổ)
        public DbSet<OTRoom> OTRooms { get; set; }
        public DbSet<OTSchedule> OTSchedules { get; set; }

        // Pharmacy (Kho Dược)
        public DbSet<Category> ItemCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<GoodsReceiptItem> GoodsReceiptItems { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 2);
            configurationBuilder.Properties<string>()
                .HaveMaxLength(250);
        }

        // ==========================================================
        // 2. CẤU HÌNH CHI TIẾT CÁC BẢNG (MODEL CREATING)
        // ==========================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Tự động áp dụng các file Configuration riêng tư (nếu có)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // 2. Tắt Cascade Delete cho mọi quan hệ khóa ngoại
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // 3. TỰ ĐỘNG LỌC DỮ LIỆU CÓ THUỘC TÍNH "ISACTIVE"
            var activeEntities = modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(IHasActiveStatus).IsAssignableFrom(e.ClrType));

            foreach (var entityType in activeEntities)
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(ApplyActiveFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.MakeGenericMethod(entityType.ClrType);
                method?.Invoke(this, new object[] { modelBuilder });
            }

            // 4. TỰ ĐỘNG LỌC DỮ LIỆU CÓ THUỘC TÍNH "ISDELETED" (XÓA MỀM)
            var softDeleteEntities = modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType));

            foreach (var entityType in softDeleteEntities)
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(ApplySoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.MakeGenericMethod(entityType.ClrType);
                method?.Invoke(this, new object[] { modelBuilder });
            }
        }


        private void ApplyActiveFilter<T>(ModelBuilder modelBuilder) where T : class, IHasActiveStatus
        {
            modelBuilder.Entity<T>().HasQueryFilter(x => x.IsActive);
        }

        private void ApplySoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : class, ISoftDelete
        {
            modelBuilder.Entity<T>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}