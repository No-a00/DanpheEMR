using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IPermissionRepository :  IGenericRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource);

        Task<bool> HasPermissionAsync(Guid roleId, string action, string resource);
        Task<bool> RoleHasPermissionAsync(Guid roleId, Guid permissionId);
        Task AddRolePermissionAsync(RolePermission rolePermission);

        Task<RolePermission> GetRolePermissionAsync(Guid roleId, Guid permissionId);
        void RemoveRolePermission(RolePermission rolePermission);
    }
}