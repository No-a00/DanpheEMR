using DanpheEMR.Application.Abstractions.Services.Admin;
using DanpheEMR.Application.Exceptions;
using DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.Core.Interface.Auth;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.DataAccess.Data; 
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DanpheEMR.DataAccess.Services.Admin
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        public AuthService(
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            IUnitOfWork unitOfWork,
            ApplicationDbContext dbContext)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
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

            
            // Truy vấn lấy danh sách chuỗi quyền dạng "TàiNguyên:HànhĐộng"
            var permissions = await _dbContext.RolePermissions
                .Include(rp => rp.Permission)     
                .Where(rp => rp.Role.UserRoles.Any(u => u.Id == user.Id) && rp.IsActive)
                .Select(rp => $"{rp.Permission.Resource}:{rp.Permission.Action}")
                .Distinct()
                .ToListAsync(cancellationToken);

            // Truyền danh sách quyền vào hàm tạo Token
            string token = _jwtProvider.GenerateToken(user, permissions);

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(0.5); 

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthenticateUserResponse
            {
                UserId = user.Id,
                Username = user.UserName,
                EmployeeId = user.EmployeeId,
                Token = token,
                RefreshToken = refreshToken 
            };
        }

        public async Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new NotFoundException("Không tìm thấy thông tin người dùng.");

            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash);
            if (!isOldPasswordValid) throw new BusinessException("Mật khẩu cũ không chính xác.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task LogoutAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;

                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<string> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

            if (user == null) throw new BusinessException("Token không tồn tại hoặc đã bị thu hồi. Vui lòng đăng nhập lại.");
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow) throw new BusinessException("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.");
            if (!user.IsActive) throw new BusinessException("Tài khoản của bạn đã bị vô hiệu hóa.");


            // Lấy lại danh sách quyền (vì trong 7 ngày qua có thể Admin đã đổi quyền của User này)
            var permissions = await _dbContext.RolePermissions
                .Include(rp => rp.Permission)
                .Where(rp => rp.Role.UserRoles.Any(u => u.Id == user.Id) && rp.IsActive)
                .Select(rp => $"{rp.Permission.Resource}:{rp.Permission.Action}")
                .Distinct()
                .ToListAsync(cancellationToken);

            string newAccessToken = _jwtProvider.GenerateToken(user, permissions);

            // Cấp lại Refresh Token mới 
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Trả về Access Token mới
            return newAccessToken;
        }

        // Hàm hỗ trợ sinh chuỗi ngẫu nhiên siêu an toàn cho Refresh Token
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}