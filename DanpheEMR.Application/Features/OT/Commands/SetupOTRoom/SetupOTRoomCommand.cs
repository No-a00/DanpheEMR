using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.OT.Commands.SetupOTRoom
{
    public record SetupOTRoomCommand(
        string RoomName,
        string Location,
        bool IsAvailable
    ) : IRequest<Result<Guid>>;
}