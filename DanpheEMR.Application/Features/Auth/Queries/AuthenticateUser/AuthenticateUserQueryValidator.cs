using FluentValidation;

namespace DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryValidator : AbstractValidator<AuthenticateUserQuery>
    {
        public AuthenticateUserQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Vui lòng nhập Tên đăng nhập.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Vui lòng nhập Mật khẩu.");
        }
    }
}