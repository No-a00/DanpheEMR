using System;

namespace DanpheEMR.Application.Features.Patients.Commands.RegisterPatient
{
    public class RegisterPatientResponse
    {
        public Guid Id { get; set; }
        public string PatientCode { get; set; } // Trả về mã bệnh nhân vừa được tạo để in ra phiếu/thẻ
        public string FullName { get; set; }
    }
}