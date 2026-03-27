using Application.Common;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RecordDonation
{
    public static class RecordDonationErrors
    {
        public static readonly Error DonorNotFound = new Error(
            "RecordDonation.DonorNotFound",
            "Không tìm thấy thông tin người hiến máu trong hệ thống.");

        public static readonly Error NotEligible = new Error(
            "RecordDonation.NotEligible",
            "Người này hiện chưa đủ điều kiện sức khỏe hoặc chưa đủ thời gian để hiến máu tiếp.");

        public static readonly Error DatabaseError = new Error(
            "RecordDonation.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin bịch máu vào kho.");
    }
}