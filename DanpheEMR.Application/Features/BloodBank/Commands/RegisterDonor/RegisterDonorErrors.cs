using Application.Common; 

namespace DanpheEMR.Application.Features.BloodBank.Commands.RegisterDonor
{
    public static class RegisterDonorErrors
    {
        public static readonly Error ContactAlreadyExists = new Error("RegisterDonor.ContactAlreadyExists", "Số điện thoại liên hệ đã tồn tại.");
        public static readonly Error InvalidBloodType = new Error("RegisterDonor.InvalidBloodType", "Nhóm máu không hợp lệ.");
        public static readonly Error DatabaseError = new Error("RegisterDonor.DatabaseError", "Đã xảy ra lỗi khi lưu vào cơ sở dữ liệu.");
        public static readonly Error Underweight = new Error("RegisterDonor.Underweight", "Người hiến máu không đủ cân nặng.");
    }
}