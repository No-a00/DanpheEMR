using Application.Common;

namespace DanpheEMR.Application.Features.EMR.Commands.CreateDoctorOrder
{
    public static class CreateDoctorOrderErrors
    {
        public static readonly Error DatabaseError = new Error(
            "CreateDoctorOrder.DatabaseError",
            "Đã xảy ra lỗi khi lưu y lệnh vào hệ thống.");
    }
}