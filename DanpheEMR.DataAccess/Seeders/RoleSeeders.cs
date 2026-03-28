using DanpheEMR.Core.Domain.Admin;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
           
            if (await context.Roles.AnyAsync())
            {
                return;
            }

            var adminRole = new Role { Id = Guid.NewGuid(), RoleName = "Admin", Description = "Quản trị viên hệ thống" };
            var receptionistRole = new Role { Id = Guid.NewGuid(), RoleName = "Receptionist", Description = "Lễ tân" };
            var doctorRole = new Role { Id = Guid.NewGuid(), RoleName = "Doctor", Description = "Bác sĩ" };
            var nurseRole = new Role { Id = Guid.NewGuid(), RoleName = "Nurse", Description = "Điều dưỡng" };

            var roles = new List<Role> { adminRole, receptionistRole, doctorRole, nurseRole };
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

            // 1. Bơm quyền cho ADMIN (Full tất cả trừ EMR)
            Assign(adminRole, "Admin", "Full");
            Assign(adminRole, "Patient", "Full");
            Assign(adminRole, "Appointment", "Full");
            Assign(adminRole, "Wards", "Full");
            Assign(adminRole, "Pharmacy", "Full");
            Assign(adminRole, "Billing", "Full");
            Assign(adminRole, "BloodBank", "Full");
            Assign(adminRole, "OT", "Full");
            Assign(adminRole, "Base", "Full");

            // 2. Bơm quyền cho DOCTOR (Bác sĩ)
            Assign(doctorRole, "Patient", "Read");
            Assign(doctorRole, "Appointment", "Read");
            Assign(doctorRole, "EMR", "Full"); 
            Assign(doctorRole, "Wards", "Write");
            Assign(doctorRole, "Pharmacy", "Read");
            Assign(doctorRole, "BloodBank", "Read");
            Assign(doctorRole, "OT", "Full");

            // 3. Bơm quyền cho NURSE (Điều dưỡng)
            Assign(nurseRole, "Patient", "Read");
            Assign(nurseRole, "Appointment", "Read");
            Assign(nurseRole, "EMR", "Write");
            Assign(nurseRole, "Wards", "Full");
            Assign(nurseRole, "Pharmacy", "Read");
            Assign(nurseRole, "BloodBank", "Read");
            Assign(nurseRole, "OT", "Write");

          

            await context.RolePermissions.AddRangeAsync(rolePermissions);

        
            await context.SaveChangesAsync();
        }
    }
}