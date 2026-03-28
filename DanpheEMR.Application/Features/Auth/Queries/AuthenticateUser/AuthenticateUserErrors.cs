using Application.Common;

namespace DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser
{
    public static class AuthenticateUserErrors
    {
        public static readonly Error InvalidCredentials = new Error(
            "Auth.InvalidCredentials",
            "Tên đăng nhập hoặc mật khẩu không chính xác.");

        public static readonly Error AccountLocked = new Error(
            "Auth.AccountLocked",
            "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Quản trị viên.");
    }
}