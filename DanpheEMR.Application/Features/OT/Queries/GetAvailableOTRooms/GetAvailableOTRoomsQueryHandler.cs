
using AutoMapper;
using DanpheEMR.Core.Interface.OT;
using MediatR;

namespace DanpheEMR.Application.Features.OT.Queries.GetAvailableOTRooms
{
    public class GetAvailableOTRoomsQueryHandler : IRequestHandler<GetAvailableOTRoomsQuery, Result<List<GetAvailableOTRoomsResponse>>>
    {
        private readonly IOTRoomRepository _otRoomRepository;
        private readonly IMapper _mapper;

        public GetAvailableOTRoomsQueryHandler(IOTRoomRepository otRoomRepository, IMapper mapper)
        {
            _otRoomRepository = otRoomRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAvailableOTRoomsResponse>>> Handle(GetAvailableOTRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _otRoomRepository.GetAvailableRoomsAsync();

            var result = _mapper.Map<List<GetAvailableOTRoomsResponse>>(rooms);

            return Result<List<GetAvailableOTRoomsResponse>>.Success(result);
        }
    }
}