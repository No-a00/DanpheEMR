using Application.Common;
using DanpheEMR.Core.Interface.EMR;
using MediatR;


namespace DanpheEMR.Application.Features.EMR.Queries.GetPendingDoctorOrders
{
    public class GetPendingDoctorOrdersQueryHandler : IRequestHandler<GetPendingDoctorOrdersQuery, Result<GetPendingDoctorOrdersResponse>>
    {
        private readonly IDoctorOrderRepository _orderRepository;

        public GetPendingDoctorOrdersQueryHandler(IDoctorOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<GetPendingDoctorOrdersResponse>> Handle(GetPendingDoctorOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
               
                var pendingOrders = await _orderRepository.GetPendingOrdersAsync();

               
                var orderDtos = pendingOrders
                    .OrderBy(o => o.OrderDate)
                    .ToDtoList();

                var response = new GetPendingDoctorOrdersResponse
                {
                    TotalPending = orderDtos.Count,
                    Orders = orderDtos
                };

                return Result<GetPendingDoctorOrdersResponse>.Success(response);
            }
            catch (Exception)
            {
                return Result<GetPendingDoctorOrdersResponse>.Failure(
                    new Error("GetPendingOrders.Error", "Đã xảy ra lỗi khi tải danh sách y lệnh chờ.")
                );
            }
        }
    }
}