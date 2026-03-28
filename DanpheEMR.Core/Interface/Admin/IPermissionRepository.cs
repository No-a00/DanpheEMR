
using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IPermissionRepository
    {
   
       
        Task<Permission> GetByIdAsync(Guid Id);
        Task<IEnumerable<Permission>> GetAllAsync();
        
        Task AddRangeAsync(IEnumerable<Permission> permissions);


        Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource);

        Task<IEnumerable<string>> GetAllDistinctResourcesAsync();


        Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);

        Task<bool> HasPermissionAsync(Guid roleId, string action, string resource);
        Task<bool> RoleHasPermissionAsync(Guid roleId, Guid permissionId);
        Task AddRolePermissionAsync(RolePermission rolePermission);
    }
}