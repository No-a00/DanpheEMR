namespace DanpheEMR.Core.Domain.Admin
{
    public interface IPermissionRepository
    {
   
       
        Task<Permission> GetByIdAsync(int id);
        Task<IEnumerable<Permission>> GetAllAsync();

       

        // 1. Lấy tất cả các Quyền thuộc về một Tài nguyên cụ thể
        // VD: Lọc Resource = "Appointment" -> Trả về 4 quyền: Create, Read, Update, Delete của Lịch khám
        Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource);

        // 2. Lấy danh sách TÊN CÁC TÀI NGUYÊN không trùng lặp (Distinct)
        // VD: Trả về danh sách ["Appointment", "Billing", "Patient", "Employee"]
        // Dùng để vẽ menu hoặc tạo các Tab/Nhóm trên giao diện phân quyền
        Task<IEnumerable<string>> GetAllDistinctResourcesAsync();

        // --- NHÓM 3: Xem hệ thống phân quyền ---

        // 3. Lấy toàn bộ danh sách Quyền MÀ MỘT VAI TRÒ (Role) ĐANG SỞ HỮU
        // (Dựa vào bảng trung gian RolePermissions)
        Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(int roleId);

        // 4. Kiểm tra nhanh xem một Role có quyền thực hiện Action trên Resource này không?
        // VD: CheckPermission(roleId: 2, action: "Delete", resource: "BillingTransaction") -> false
        Task<bool> HasPermissionAsync(int roleId, string action, string resource);
    }
}