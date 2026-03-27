using Application.Common;
using DanpheEMR.Core.Interface.BloodBank; // Nơi chứa IBloodDonorRepository
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetEligibleDonors
{
    public class GetEligibleDonorsQueryHandler : IRequestHandler<GetEligibleDonorsQuery, Result<GetEligibleDonorsResponse>>
    {
        private readonly IBloodDonoreRepository _donorRepository;

        public GetEligibleDonorsQueryHandler(IBloodDonoreRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<Result<GetEligibleDonorsResponse>> Handle(GetEligibleDonorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
               
                var eligibleDonors = await _donorRepository.GetEligibleDonorsAsync(request.BloodGroupId);

                var donorDtos = eligibleDonors.ToDtoList();

                var response = new GetEligibleDonorsResponse
                {
                    TotalDonors = donorDtos.Count,
                    Donors = donorDtos
                };

                return Result<GetEligibleDonorsResponse>.Success(response);
            }
            catch (Exception)
            {
                return Result<GetEligibleDonorsResponse>.Failure(new Error("GetEligibleDonors.Error", "Đã xảy ra lỗi khi tìm kiếm người hiến máu."));
            }
        }
    }
}