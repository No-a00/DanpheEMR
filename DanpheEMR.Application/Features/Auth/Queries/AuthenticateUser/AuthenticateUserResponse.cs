using System;

namespace DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser
{
    public class AuthenticateUserResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid? EmployeeId { get; set; }

        public string Token { get; set; }
    }
}