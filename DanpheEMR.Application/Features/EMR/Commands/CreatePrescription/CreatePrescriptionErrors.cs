using Application.Common;

namespace DanpheEMR.Application.Features.EMR.Commands.CreatePrescription
{
    public static class CreatePrescriptionErrors
    {
        public static readonly Error DatabaseError = new Error(
            "CreatePrescription.DatabaseError",
            "Đã xảy ra lỗi khi lưu đơn thuốc vào cơ sở dữ liệu.");

        public static readonly Error EmptyItems = new Error(
            "CreatePrescription.EmptyItems",
            "Đơn thuốc phải có ít nhất một loại thuốc.");
    }
}