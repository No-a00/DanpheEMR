using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.OT;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.OT.Commands.SetupOTRoom
{
    public class SetupOTRoomHandler : IRequestHandler<SetupOTRoomCommand, Result<Guid>>
    {
        private readonly IOTRoomRepository _otRoomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetupOTRoomHandler(IOTRoomRepository otRoomRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _otRoomRepository = otRoomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(SetupOTRoomCommand request, CancellationToken cancellationToken)
        {
            if (await _otRoomRepository.IsRoomNameExistsAsync(request.RoomName))
                return Result<Guid>.Failure(SetupOTRoomErrors.RoomNameExists);

            var room = _mapper.Map<OTRoom>(request);
            await _otRoomRepository.AddAsync(room);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return saveResult > 0 ? Result<Guid>.Success(room.Id) : Result<Guid>.Failure(SetupOTRoomErrors.DatabaseError);
        }
    }
}