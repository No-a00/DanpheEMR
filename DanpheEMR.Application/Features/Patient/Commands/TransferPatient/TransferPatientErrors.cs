using Application.Common;

namespace DanpheEMR.Application.Features.Patients.Commands.TransferPatient
{
    public static class TransferPatientErrors
    {
        public static readonly Error InvalidDepartment = new Error("Transfer.InvalidDept", "Khoa chuyển đến không được trùng với khoa hiện tại.");
        public static readonly Error AdmissionNotFound = new Error("Transfer.AdmissionNotFound", "Không tìm thấy hồ sơ bệnh án nội trú.");
        public static readonly Error DBError = new Error("Transfer.DBError", "Lỗi khi lưu lệnh chuyển khoa.");
    }
}