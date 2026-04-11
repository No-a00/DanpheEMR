using Application.Common;
using DanpheEMR.Application.Common.Models;
using DanpheEMR.Core.Interface.Admin;
using MediatR;


namespace DanpheEMR.Application.Features.Auth.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, ApiResponse<UserProfileDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetMyProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserProfileDto>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
           
            var user = await _userRepository.GetByIdAsync(request.UserId);

            
            if (user == null)
            {
                return ApiResponse<UserProfileDto>.Failure(new List<Error> {
                    new Error("Auth.UserNotFound", "Không tìm thấy thông tin tài khoản.")
                });
            }

            var dto = new UserProfileDto
            {
                Id = user.Id,
                Username = user.UserName, 
                Email = user.Email ?? string.Empty, 
                FullName = user.FullName ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                AvatarUrl = user.AvatarUrl ?? string.Empty,
                DateOfBirth = user.DateOfBirth ,
                Gender = user.Gender ,


                IsActive = user.IsActive
            };

            return ApiResponse<UserProfileDto>.Success(dto, "Lấy dữ liệu thành công");
        }
    }
}