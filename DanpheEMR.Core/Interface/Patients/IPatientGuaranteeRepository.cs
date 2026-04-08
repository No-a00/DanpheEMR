using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interface.Patients
{
    public interface IPatientGuaranteeRepository : IGenericRepository<PatientGuarantee>
    {
        // Xem TOÀN BỘ lịch sử bảo lãnh của bệnh nhân này từ trước đến nay
        Task<IEnumerable<PatientGuarantee>> GetAllGuaranteesByPatientIdAsync(Guid patientId);

        //  Chỉ lấy những giấy bảo lãnh CÒN HIỆU LỰC (Dùng cho Thu ngân chốt bill)
        Task<IEnumerable<PatientGuarantee>> GetActiveGuaranteesByPatientIdAsync(Guid patientId);

        // Tìm xem CMND/CCCD này đang bảo lãnh cho những ai
        Task<IEnumerable<PatientGuarantee>> GetGuaranteedPatientsByIdCardAsync(string idCardNumber);
    }
}