using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Admin
{
    public class AuditLog : BaseEntity
    {
        public Guid Id { get; set; }
        public string RecordId { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
