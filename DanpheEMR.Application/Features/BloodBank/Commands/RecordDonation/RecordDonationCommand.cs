using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RecordDonation
{
    public record RecordDonationCommand(
        Guid DonorId,        
        string BagNumber,    
        int VolumeInMl      
    ) : IRequest<Result<Guid>>; 
}