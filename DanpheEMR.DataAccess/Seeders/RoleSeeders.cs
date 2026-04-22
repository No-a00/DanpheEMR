using DanpheEMR.Core.Domain.Admin;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            // 1. Kiểm tra nếu đã có dữ liệu thì không làm gì cả
            if (await context.Roles.AnyAsync())
            {
                return;
            }

            var adminRole = new Role { Id = Guid.NewGuid(), RoleName = "Admin", Description = "Quản trị viên hệ thống" };
            var receptionistRole = new Role { Id = Guid.NewGuid(), RoleName = "Receptionist", Description = "Lễ tân" };
            var doctorRole = new Role { Id = Guid.NewGuid(), RoleName = "Doctor", Description = "Bác sĩ" };
            var nurseRole = new Role { Id = Guid.NewGuid(), RoleName = "Nurse", Description = "Điều dưỡng" };
            var accountantRole = new Role { Id = Guid.NewGuid(), RoleName = "Accountant", Description = "Kế toán" };
            var pharmacistRole = new Role { Id = Guid.NewGuid(), RoleName = "Pharmacist", Description = "Dược sĩ" };
            var techRole = new Role { Id = Guid.NewGuid(), RoleName = "Tech", Description = "KTV Xét nghiệm" };


            var roles = new List<Role> {
                adminRole, receptionistRole, doctorRole, nurseRole,
                accountantRole, pharmacistRole, techRole
            };
            await context.Roles.AddRangeAsync(roles);


            var resources = new[] { "Admin", "Patient", "Appointment", "EMR", "Wards", "Pharmacy", "Billing", "BloodBank", "OT", "Base" };
            var actions = new[] { "Full", "Write", "Read" };

            var allPermissions = new List<Permission>();
            foreach (var r in resources)
            {
                foreach (var a in actions)
                {
                    allPermissions.Add(new Permission
                    {
                        Id = Guid.NewGuid(),
                        Resource = r,
                        Action = a,
                        Description = $"Quyền {a} phân hệ {r}"
                    });
                }
            }
            await context.Permissions.AddRangeAsync(allPermissions);

            // Phải lưu Roles và Permissions trước để SQL Server xác nhận các ID này tồn tại
            await context.SaveChangesAsync();

            var rolePermissions = new List<RolePermission>();

            void Assign(Role role, string resource, string action)
            {
                var permission = allPermissions.First(p => p.Resource == resource && p.Action == action);
                rolePermissions.Add(new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                    IsActive = true
                });
            }

            //  Admin (Full tất cả trừ EMR)
            foreach (var res in resources.Where(x => x != "EMR")) Assign(adminRole, res, "Full");

            // Doctor (Bác sĩ)
            Assign(doctorRole, "Patient", "Read");
            Assign(doctorRole, "Appointment", "Read");
            Assign(doctorRole, "EMR", "Full");
            Assign(doctorRole, "Wards", "Write");
            Assign(doctorRole, "Pharmacy", "Read");
            Assign(doctorRole, "BloodBank", "Read");
            Assign(doctorRole, "OT", "Full");

            //  Nurse (Điều dưỡng)
            Assign(nurseRole, "Patient", "Read");
            Assign(nurseRole, "Appointment", "Read");
            Assign(nurseRole, "EMR", "Write");
            Assign(nurseRole, "Wards", "Full");
            Assign(nurseRole, "Pharmacy", "Read");
            Assign(nurseRole, "BloodBank", "Read");
            Assign(nurseRole, "OT", "Write");

            //  Accountant (Kế toán)
            Assign(accountantRole, "Patient", "Read");
            Assign(accountantRole, "Billing", "Full");

            //  Pharmacist (Dược sĩ)
            Assign(pharmacistRole, "Patient", "Read");
            Assign(pharmacistRole, "Pharmacy", "Full");

            //  Tech (KTV Xét nghiệm)
            Assign(techRole, "Patient", "Read");
            Assign(techRole, "BloodBank", "Full");


            await context.RolePermissions.AddRangeAsync(rolePermissions);
            await context.SaveChangesAsync();
        }
    }
}