using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPendingDoctorOrders
{
    public class GetPendingDoctorOrdersResponse
    {
        public int TotalPending { get; set; }
        public List<PendingOrderDto> Orders { get; set; } = new();
    }

    public class PendingOrderDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderText { get; set; }

        public string PatientName { get; set; }

        public string ProviderName { get; set; }
    }
}