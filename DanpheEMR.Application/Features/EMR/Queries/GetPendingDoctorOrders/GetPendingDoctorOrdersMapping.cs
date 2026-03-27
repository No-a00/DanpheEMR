using DanpheEMR.Core.Domain.EMR;
using System.Collections.Generic;
using System.Linq;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPendingDoctorOrders
{
    public static class GetPendingDoctorOrdersMapping
    {
        public static PendingOrderDto ToDto(this DoctorOrder order)
        {
            return new PendingOrderDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                OrderText = order.OrderText,

                ProviderName = order.Provider != null ? $"{order.Provider.FirstName} {order.Provider.LastName}" : "N/A",


                PatientName = order.Visit?.Patient != null ? $"{order.Visit.Patient.FirstName} {order.Visit.Patient.LastName}" : "N/A"
            };
        }

        public static List<PendingOrderDto> ToDtoList(this IEnumerable<DoctorOrder> orders)
        {
            return orders.Select(o => o.ToDto()).ToList();
        }
    }
}