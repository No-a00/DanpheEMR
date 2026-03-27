
using Application.Common; 
namespace DanpheEMR.Application.Features.EMR.Commands.AddProgressNote
{
    public  static class AddProgressNoteErrors
    {

        public static readonly Error DatabaseError = new Error(
            "AddDiagnosis.DatabaseError",
            "Đã xảy ra lỗi khi lưu Tiến độ vào hồ sơ bệnh án.");
    }
}
