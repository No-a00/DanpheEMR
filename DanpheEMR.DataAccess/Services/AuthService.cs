using DanpheEMR.Application.Abstractions.Services.Admin;
using DanpheEMR.Application.Exceptions;
using DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser; 
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.Core.Interface.Auth;
using DanpheEMR.Application.Abstractions.Persistence;


namespace DanpheEMR.DataAccess.Services.Admin
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticateUserResponse> LoginAsync(string username, string password, CancellationToken cancellationToken)
        {
           
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !user.IsActive)
            {
                return null; 
            }

           
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }

            string token = _jwtProvider.GenerateToken(user);

            return new AuthenticateUserResponse
            {
                UserId = user.Id,
                Username = user.UserName,
                EmployeeId = user.EmployeeId,
                Token = token
            };
        }


        public async Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("Không tìm thấy thông tin người dùng.");
            }

           
            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash);
            if (!isOldPasswordValid)
            {
                
                throw new BusinessException("Mật khẩu cũ không chính xác.");
            }

            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

          
            _userRepository.Update(user); 
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

      
        public async Task LogoutAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                // Giả sử Entity User của bạn có lưu RefreshToken
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;

                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

     
        public async Task<string> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
         
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

            if (user == null)
            {
                throw new BusinessException("Token không tồn tại hoặc đã bị thu hồi. Vui lòng đăng nhập lại.");
            }

           
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new BusinessException("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.");
            }

            
            if (!user.IsActive)
            {
                throw new BusinessException("Tài khoản của bạn đã bị vô hiệu hóa.");
            }

            
            string newAccessToken = _jwtProvider.GenerateToken(user);

            /* Lưu ý bảo mật (Refresh Token Rotation): 
               Nhiều hệ thống an toàn sẽ sinh luôn cả REFRESH TOKEN MỚI ở bước này và lưu đè xuống DB, 
               chứ không dùng lại Refresh Token cũ. Nếu thiết kế như vậy, hàm này phải trả về 2 giá trị.
               Tạm thời ở đây ta chỉ trả về Access Token mới theo Interface hiện tại.
            */

            return newAccessToken;
        }
    }
}