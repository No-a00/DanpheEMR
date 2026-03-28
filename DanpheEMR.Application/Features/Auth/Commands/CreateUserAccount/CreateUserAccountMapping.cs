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
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = hashedPassword, 
                IsActive = true, 
                EmployeeId = command.EmployeeId
            };
        }
    }
}