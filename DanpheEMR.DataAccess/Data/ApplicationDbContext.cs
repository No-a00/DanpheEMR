using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DanpheEMR.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        // 1. Admin & Phân quyền (Security)
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SystemParameter> SystemParameters { get; set; }

        // 2. Appointment (Lịch khám & Đặt chỗ)
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        // 3. Billing (Thu ngân & Viện phí)
        public DbSet<BillingTransaction> BillingTransactions { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        // 4. BloodBank (Ngân hàng máu)
        public DbSet<BloodDonor> BloodDonors { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<BloodInventory> BloodInventories { get; set; }
        public DbSet<BloodIssue> BloodIssues { get; set; }

        // 5. EMR (Bệnh án điện tử & Cận lâm sàng)
        public DbSet<ClinicalNote> ClinicalNotes { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<DoctorOrder> DoctorOrders { get; set; }
        public DbSet<MedicationAdministration> MedicationAdministrations { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<ProgressNote> ProgressNotes { get; set; }
        public DbSet<Vitals> Vitals { get; set; }

        // 6. Patient (Hành chính & Tiếp đón)
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddresses { get; set; }
        public DbSet<PatientKin> PatientKins { get; set; }
        public DbSet<PatientGuarantee> PatientGuarantees { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        // 7. Ward / Bed (Nội trú)
        public DbSet<Ward> Wards { get; set; }
        public DbSet<BedFeature> BedFeatures { get; set; }
        public DbSet<Bed> Beds { get; set; }

        // 8. OT (Phòng mổ)
        public DbSet<OTRoom> OTRooms { get; set; }
        public DbSet<OTSchedule> OTSchedules { get; set; }

        // 9. Pharmacy (Kho Dược)
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
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
            configurationBuilder.Properties<string>().HaveMaxLength(250);
        }

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