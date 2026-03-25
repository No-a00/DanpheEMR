using DanpheEMR.Core.Domain.EMR;


namespace DanpheEMR.Application.Features.EMR.Commands.CreatePrescription
{
    public static class CreatePrescriptionMapping
    {
        public static Prescription ToEntity(this CreatePrescriptionCommand command)
        {
            var prescriptionId = Guid.NewGuid();

            return new Prescription
            {
                Id = prescriptionId,
                Notes = command.Notes,
                VisitId = command.VisitId,
                PatientId = command.PatientId,
                PrescriberId = command.PrescriberId,

                
                PrescriptionDate = DateTime.Now,
                Status = "Active", // Hoặc "Pending" tùy quy trình của bạn
                IsActive = true, 
                CancelReason = null,
                UserIdCancel = Guid.Empty,

               
                
                Items = command.Items?.Select(dto => new PrescriptionItem
                {
                    Id = Guid.NewGuid(),
                    PrescriptionId = prescriptionId, // Gắn khóa ngoại
                    MedicineId = dto.MedicineId,
                    Dosage = dto.Dosage,
                    Frequency = dto.Frequency,
                    DurationInDays = dto.DurationInDays
                }).ToList()
            };
        }
    }
}