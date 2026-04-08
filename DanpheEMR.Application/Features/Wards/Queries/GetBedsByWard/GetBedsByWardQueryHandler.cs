using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Interface.Inpatient;
using DanpheEMR.Core.Interface.Wards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetBedsByWard
{
    public class GetBedsByWardQueryHandler : IRequestHandler<GetBedsByWardQuery, Result<List<GetBedsByWardResponse>>>
    {
        private readonly IBedRepository _bedRepository;
        private readonly IMapper _mapper;

        public GetBedsByWardQueryHandler(IBedRepository bedRepository, IMapper mapper)
        {
            _bedRepository = bedRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetBedsByWardResponse>>> Handle(GetBedsByWardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var beds = await _bedRepository.GetBedsByWardIdAsync(request.WardId);
                var result = _mapper.Map<List<GetBedsByWardResponse>>(beds);

                return Result<List<GetBedsByWardResponse>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<GetBedsByWardResponse>>.Failure(new Error("Bed.Exception", $"Lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}