using DanpheEMR.Core.Domain.EMR;
using System;

namespace DanpheEMR.Application.Features.EMR.Commands.CreateDoctorOrder
{
    public static class CreateDoctorOrderMapping
    {
        public static DoctorOrder ToEntity(this CreateDoctorOrderCommand command)
        {
            return new DoctorOrder
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderText = command.OrderText,
                Status = "Pending",
                IsDeleted = false,    

                VisitId = command.VisitId,
                ProviderId = command.ProviderId
            };
        }
    }
}