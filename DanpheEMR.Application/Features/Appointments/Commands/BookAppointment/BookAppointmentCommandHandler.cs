
using Application.Features.Appointments.Commands.BookAppointment;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Application.Features.Appointments.Commands.BookAppointment;
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.Core.Interfaces.Appointment;
namespace Application.Features.Appointment.Commands.BookAppointment;

public sealed class BookAppointmentCommandHandler
    : BaseHandler<BookAppointmentCommand, Result<BookAppointmentResponse>>
{

    private readonly IAppointmentRepository _appointmentRepo;

    private readonly IPatientRepository _patientRepo;

    private readonly IDoctorScheduleRepository _scheduleRepo;

    private readonly IUnitOfWork _unitOfWork;


    public BookAppointmentCommandHandler(

        IAppointmentRepository appointmentRepo,

        IPatientRepository patientRepo,

        IDoctorScheduleRepository scheduleRepo,

        IUnitOfWork unitOfWork)

    {

        _appointmentRepo = appointmentRepo;

        _patientRepo = patientRepo;

        _scheduleRepo = scheduleRepo;

        _unitOfWork = unitOfWork;

    }


    public override async Task<Result<BookAppointmentResponse>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
    {
        // 1. Kiểm tra bệnh nhân (Đảm bảo IRepository dùng Guid)
        var patient = await _patientRepo.GetByPatientCodeAsync(request.PatientCode);
        if (patient == null)
            return Result<BookAppointmentResponse>.Failure(BookAppointmentErrors.PatientNotFound);

        // 2. Kiểm tra lịch bác sĩ
        var doctorSchedule = await _scheduleRepo.GetDoctorScheduleByCodeAsync(request.DocTorCode, request.AppointmentDate);
        if (doctorSchedule == null)
            return Result<BookAppointmentResponse>.Failure(BookAppointmentErrors.DoctorNotFound);

        // 3. Kiểm tra bác sĩ bận (Phải khai báo hàm này trong Interface)
        bool isBusy = await _appointmentRepo.IsDoctorBusy(request.DocTorCode, request.AppointmentDate);
        if (isBusy)
            return Result<BookAppointmentResponse>.Failure(BookAppointmentErrors.ScheduleConflict);

        // 4. Tạo Entity (Đảm bảo Class Appointment ở Core có đủ các trường này)
        var appointment = new DanpheEMR.Core.Domain.Appointments.Appointment
        {
            Id = Guid.NewGuid(),
            PatientCode =  request.PatientCode,
            DoctorCode = request.DocTorCode,
            AppointmentDate = request.AppointmentDate,
            Reason = request.Reason,
            CreatedAt = DateTime.UtcNow
        };

        await _appointmentRepo.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 5. Trả về Success (Dùng ngoặc đơn vì nó là Method)
        return Result.Success(new BookAppointmentResponse
        {
            AppointmentId = appointment.Id,
            Message = "Thành công!"
        });
    }
}