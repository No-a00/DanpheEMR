using Application.Common;

namespace DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount
{
    public static class CreateUserAccountErrors
    {
        public static readonly Error UsernameExists = new Error(
            "CreateUserAccount.UsernameExists",
            "Tên đăng nhập này đã tồn tại trong hệ thống.");

        public static readonly Error EmailExists = new Error(
            "CreateUserAccount.EmailExists",
            "Email này đã được sử dụng cho một tài khoản khác.");

        public static readonly Error DatabaseError = new Error(
            "CreateUserAccount.DatabaseError",
            "Đã xảy ra lỗi khi tạo tài khoản người dùng.");
    }
}