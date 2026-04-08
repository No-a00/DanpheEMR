using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.AddSupplier
{
    public record AddSupplierCommand(
        string SupplierName,
        string ContactPerson,
        string PhoneNumber,
        string Email,
        string Address
    ) : IRequest<Result<Guid>>;
}