
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;
using Application.Common;

namespace DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount
{
    public class CreateUserAccountHandler : IRequestHandler<CreateUserAccountCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserAccountHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
             
                bool isUsernameExist = await _userRepository.IsUsernameUniqueAsync(request.UserName) == false;
                if (isUsernameExist)
                {
                    return Result<Guid>.Failure(CreateUserAccountErrors.UsernameExists);
                }

               
                bool isEmailExist = await _userRepository.IsEmailUniqueAsync(request.Email) == false;
                if (isEmailExist)
                {
                    return Result<Guid>.Failure(CreateUserAccountErrors.EmailExists);
                }
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = request.ToEntity(hashedPassword);

                await _userRepository.AddAsync(user);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(user.Id);
                }

                return Result<Guid>.Failure(CreateUserAccountErrors.DatabaseError);
            }
            catch (Exception ex)
            {
                // Lấy lỗi gốc từ Entity Framework (nếu có)
                string realError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                // Tạm thời trả về lỗi này để debug
                return Result<Guid>.Failure(new Error("Debug.SQL_Error", realError));
            }
        }
    }
}