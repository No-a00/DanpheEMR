using DanpheEMR.Core.Domain.Admin;
using System;

namespace DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount
{
    public static class CreateUserAccountMapping
    {
        public static User ToEntity(this CreateUserAccountCommand command, string hashedPassword)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Code = command.Code,
                UserName = command.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                FullName = command.FullName,
                Gender = command.Gender,
                DateOfBirth = command.DateOfBirth,
                AvatarUrl = command.AvatarUrl,
                PasswordHash = hashedPassword, 
                IsActive = true, 
            };
        }
    }
}