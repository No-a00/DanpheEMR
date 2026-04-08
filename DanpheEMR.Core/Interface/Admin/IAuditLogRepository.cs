
using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Interface.Admin
{
    public class AuditLogFilter
    {
        public string TableName { get; set; }
        public Guid? UserId { get; set; }
        public string Action { get; set; } 
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public interface IAuditLogRepository
    {
       
        Task AddAsync(AuditLog auditLog);

       
        // Tìm kiếm tổng hợp có phân trang (tránh treo máy khi bảng có 10 triệu dòng)
        Task<IEnumerable<AuditLog>> SearchLogsAsync(AuditLogFilter filter, int pageNumber, int pageSize);
        //  Truy vết lịch sử của MỘT dòng dữ liệu cụ thể
        Task<IEnumerable<AuditLog>> GetHistoryOfRecordAsync(string tableName, string recordId);
    }
}