using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Interface.Wards;
using MediatR;


namespace DanpheEMR.Application.Features.Inpatient.Queries.GetAvailableBeds
{
    public class GetAvailableBedsQueryHandler : IRequestHandler<GetAvailableBedsQuery, Result<List<GetAvailableBedsResponse>>>
    {
        private readonly IBedRepository _bedRepository;
        private readonly IMapper _mapper;

        public GetAvailableBedsQueryHandler(IBedRepository bedRepository, IMapper mapper)
        {
            _bedRepository = bedRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAvailableBedsResponse>>> Handle(GetAvailableBedsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Hàm này cần được tạo trong IBedRepository: Lấy giường có Status == Available
                var beds = await _bedRepository.GetAvailableBedsByWardAsync(request.WardId);
                var result = _mapper.Map<List<GetAvailableBedsResponse>>(beds);

                return Result<List<GetAvailableBedsResponse>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<GetAvailableBedsResponse>>.Failure(new Error("Bed.Exception", $"Lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}