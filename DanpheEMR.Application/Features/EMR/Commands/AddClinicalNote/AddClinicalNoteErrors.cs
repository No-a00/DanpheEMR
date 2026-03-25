using Application.Common;

namespace DanpheEMR.Application.Features.EMR.Commands.AddClinicalNote
{
    public static class AddClinicalNoteErrors
    {
        public static readonly Error DatabaseError = new Error(
            "AddClinicalNote.DatabaseError",
            "Đã xảy ra lỗi khi lưu Clinical Note vào cơ sở dữ liệu.");

        public static readonly Error PatientNotFound = new Error(
            "AddClinicalNote.PatientNotFound",
            "Không tìm thấy thông tin bệnh nhân.");
    }
}