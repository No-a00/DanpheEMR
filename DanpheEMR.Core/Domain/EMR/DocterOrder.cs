using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.ADTModels;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class DocterOrder : BaseEntity
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; } // Ngày giờ tạo đơn
        public string OrderText { get; set; } = string.Empty; // Nội dung đơn thuốc hoặc chỉ định
        public string Status { get; set; }  // Trạng thái đơn: Pending, Completed, Cancelled
        public int VisitId { get; set; }
        public int ProviderId { get; set; }

        public Visit Visit { get; set; }
        public Employee Provider { get; set; }
    }
}
