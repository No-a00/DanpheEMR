using DanpheEMR.Core.Domain.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanpheEMR.DataAccess.Configurations.Admin
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // 1. Cấu hình khóa chính và thuộc tính  
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);

            // 2. Quan hệ 1-N với Department (Phòng ban)
            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // Tuyệt đối không xóa dây chuyền

            // 3. Quan hệ 1-N với DoctorSchedule (Lịch trực)
            builder.HasMany(e => e.Schedules)
                   .WithOne(s => s.Provider) // Giả định trong DoctorSchedule có property Employee
                   .HasForeignKey(s => s.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 4. Quan hệ với User (Một nhân viên có thể có 1 hoặc nhiều account)
            builder.HasMany(e => e.Users)
                   .WithOne(u => u.Employee)
                   .HasForeignKey(u => u.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 5. Index cho các cột hay tìm kiếm để tăng tốc độ truy vấn
            builder.HasIndex(e => e.ContactNumber);
        }
    }
}