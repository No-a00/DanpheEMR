using Application.Common;

namespace DanpheEMR.Application.Features.Inpatient.Commands.AddBed
{
    public static class AddBedErrors
    {
        public static readonly Error DBError = new Error("Bed.DBError", "Lỗi khi lưu thông tin giường bệnh.");
    }
}