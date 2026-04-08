using Application.Common;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.Wards;
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetWardOccupancy
{
    public class GetWardOccupancyQueryHandler : IRequestHandler<GetWardOccupancyQuery, Result<GetWardOccupancyResponse>>
    {
        private readonly IWardRepository _wardRepository;

        public GetWardOccupancyQueryHandler(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }

        public async Task<Result<GetWardOccupancyResponse>> Handle(GetWardOccupancyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var ward = await _wardRepository.GetWardWithBedsAsync(request.WardId);

                if (ward == null)
                    return Result<GetWardOccupancyResponse>.Failure(new Error("Ward.NotFound", "Không tìm thấy buồng bệnh."));

                int totalBeds = ward.Beds.Count;
                int occupiedBeds = ward.Beds.Count(b => b.Status == BedStatus.Occupied);
                int availableBeds = ward.Beds.Count(b => b.Status == BedStatus.Available);

                
                double occupancyRate = totalBeds > 0 ? Math.Round((double)occupiedBeds / totalBeds * 100, 2) : 0;

                var response = new GetWardOccupancyResponse(
                    ward.Id,
                    ward.WardName,
                    totalBeds,
                    occupiedBeds,
                    availableBeds,
                    occupancyRate
                );

                return Result<GetWardOccupancyResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<GetWardOccupancyResponse>.Failure(new Error("Occupancy.Exception", $"Lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}