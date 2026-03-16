using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Patients
{
    public interface IPatientGuaranteeRepository
    {
        Task<PatientGuarantee> GetByIdAsync(int id);
        Task<PatientGuarantee> AddAsync(PatientGuarantee guarantee);
        Task UpdateAsync(PatientGuarantee guarantee);
        Task CancelGuaranteeAsync(int id, string cancelReason, int cancelledByUserId);


        // Xem TOÀN BỘ lịch sử bảo lãnh của bệnh nhân này từ trước đến nay
        Task<IEnumerable<PatientGuarantee>> GetAllGuaranteesByPatientIdAsync(int patientId);

        //  Chỉ lấy những giấy bảo lãnh CÒN HIỆU LỰC (Dùng cho Thu ngân chốt bill)
        Task<IEnumerable<PatientGuarantee>> GetActiveGuaranteesByPatientIdAsync(int patientId);

        // Tìm xem CMND/CCCD này đang bảo lãnh cho những ai
        Task<IEnumerable<PatientGuarantee>> GetGuaranteedPatientsByIdCardAsync(string idCardNumber);
    }
}