using Application.Common;

namespace DanpheEMR.Application.Features.OT.Commands.SetupOTRoom
{
    public static class SetupOTRoomErrors
    {
        public static readonly Error RoomNameExists = new Error("SetupOTRoom.RoomNameExists", "Tên phòng mổ này đã tồn tại.");
        public static readonly Error DatabaseError = new Error("SetupOTRoom.DatabaseError", "Lỗi khi lưu phòng mổ vào CSDL.");
    }
}