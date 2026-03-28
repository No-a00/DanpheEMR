using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupBranch
{
    public record SetupBranchCommand(
        string BranchName,
        string Address,
        string ContactEmail
    ) : IRequest<Result<Guid>>; 
}