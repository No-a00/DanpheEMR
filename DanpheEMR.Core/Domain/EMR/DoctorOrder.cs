using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.EMR
{
    public class DoctorOrder : BaseEntity
    {
        public int  Id { get; set; }
        public DateTime OrderDate { get; set; } // Ngày giờ tạo đơn
        public string OrderText { get; set; } = string.Empty; // Nội dung đơn thuốc hoặc chỉ định
        public string Status { get; set; }  // Trạng thái đơn: Pending, Completed, Cancelled
        public int VisitId { get; set; }
        //hủy và lí do
        public int cancelledByUserId { get; set; }
        public bool isActive { get; set; }
        public string cancelReason { get; set; }
        //
        public int ProviderId { get; set; }

        public Visit Visit { get; set; }
        public Employee Provider { get; set; }
    }
}
