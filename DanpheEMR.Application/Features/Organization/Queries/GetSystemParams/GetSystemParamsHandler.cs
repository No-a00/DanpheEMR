
using AutoMapper;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Queries.GetSystemParams
{
    public class GetSystemParamsHandler : IRequestHandler<GetSystemParamsQuery, Result<List<GetSystemParamsResponse>>>
    {
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IMapper _mapper;

        public GetSystemParamsHandler(ISystemParameterRepository systemParameterRepository, IMapper mapper)
        {
            _systemParameterRepository = systemParameterRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetSystemParamsResponse>>> Handle(GetSystemParamsQuery request, CancellationToken cancellationToken)
        {
            var parameters = await _systemParameterRepository.GetAllAsync();

            var query = parameters.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.ToLower();
                query = query.Where(p => p.ParameterName.ToLower().Contains(search));
            }
            var result = _mapper.Map<List<GetSystemParamsResponse>>(query.ToList());

            return Result<List<GetSystemParamsResponse>>.Success(result);
        }
    }
}