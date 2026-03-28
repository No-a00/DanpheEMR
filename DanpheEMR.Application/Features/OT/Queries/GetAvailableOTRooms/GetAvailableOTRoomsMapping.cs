using AutoMapper;
using DanpheEMR.Core.Domain.OT;

namespace DanpheEMR.Application.Features.OT.Queries.GetAvailableOTRooms
{
    public class GetAvailableOTRoomsMapping : Profile
    {
        public GetAvailableOTRoomsMapping()
        {
            CreateMap<OTRoom, GetAvailableOTRoomsResponse>();
        }
    }
}