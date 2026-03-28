using System;

namespace DanpheEMR.Application.Features.OT.Queries.GetAvailableOTRooms
{
    public class GetAvailableOTRoomsResponse
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string Location { get; set; }
    }
}