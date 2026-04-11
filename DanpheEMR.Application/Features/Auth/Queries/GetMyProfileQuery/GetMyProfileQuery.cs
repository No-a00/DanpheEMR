using DanpheEMR.Application.Common.Models;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Auth.Queries.GetMyProfile
{
    public class GetMyProfileQuery : IRequest<ApiResponse<UserProfileDto>>
    {
        public Guid UserId { get; set; }

        public GetMyProfileQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}