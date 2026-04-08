using DanpheEMR.Core.Domain.EMR;
using System;

namespace DanpheEMR.Application.Features.EMR.Commands.RecordVitals
{
    public static class RecordVitalsMapping
    {
        public static Vitals ToEntity(this RecordVitalsCommand command)
        {
           
            decimal heightInMeters = command.Height / 100m;
            decimal bmiValue = 0;

            if (heightInMeters > 0)
            { 
                bmiValue = Math.Round(command.Weight / (heightInMeters * heightInMeters), 2);
            }

            return new Vitals
            {
                Id = Guid.NewGuid(),
                HeartRate = command.HeartRate,
                BloodPressure = command.BloodPressure,
                Temperature = command.Temperature,
                RespiratoryRate = command.RespiratoryRate,
                SpO2 = command.SpO2,
                Weight = command.Weight,
                Height = command.Height,
                BMI = bmiValue, 

                VisitId = command.VisitId,
                PatientId = command.PatientId,

             
                RecordedAt = DateTime.Now,
                IsDeleted = false,
                Reason = null,
                DeletedBy = Guid.Empty
            };
        }
    }
}