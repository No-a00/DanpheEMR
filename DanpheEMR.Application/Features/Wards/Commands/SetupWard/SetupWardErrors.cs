using Application.Common;

namespace DanpheEMR.Application.Features.Inpatient.Commands.SetupWard
{
    public static class SetupWardErrors
    {
        public static readonly Error DBError = new Error("Ward.DBError", "Lỗi khi lưu buồng bệnh.");
    }
}