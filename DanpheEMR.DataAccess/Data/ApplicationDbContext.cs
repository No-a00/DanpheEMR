using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Domain.Pharnacy;
using DanpheEMR.Core.Domain.Wards;
using Microsoft.EntityFrameworkCore;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ... Giữ lại các cấu hình đặc biệt cho OTSchedule và Transfer (nếu cần) ...

            // GIẢI PHÁP TỔNG THỂ: Tắt Cascade Delete cho mọi quan hệ khóa ngoại
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            // tự động ẩn soft Delete all Project
            modelBuilder.Entity<BloodDonor>().HasQueryFilter(x => !x.IsDeleted);

            // Tự động cấu hình kiểu Decimal (Giữ lại đoạn này của bạn)
            var decimalProperties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in decimalProperties)
            {
                property.SetColumnType("decimal(18,2)");
            }
        }


    }
}