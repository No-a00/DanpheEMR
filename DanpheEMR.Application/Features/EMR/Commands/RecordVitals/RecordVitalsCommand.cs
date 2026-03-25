using MediatR;
namespace DanpheEMR.Application.Features.EMR.Commands.RecordVitals
{
    public record RecordVitalsCommand(
        int HeartRate,
        string BloodPressure,
        decimal Temperature,
        int RespiratoryRate,
        decimal SpO2,
        decimal Weight,
        decimal Height,
        Guid VisitId,
        Guid PatientId
    ) : IRequest<Result<RecordVitalsResponse>>;
}