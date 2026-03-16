using DanpheEMR.WEB.Iterface.Repository;
using DanpheEMR.WEB.Models;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.WEB.Repository
{
    public class MedicalHistoryrepository : GenericRepository<MedicalHistoryModel>, IMedicalHistoryRepository
    {
        public MedicalHistoryrepository(DbContext context) : base(context)
        {
        }
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryByPatientIdAsync(int patientId)
        {
            return _context.Set<MedicalHistoryModel>()
                           .Where(mh => mh.PatientId == patientId)
                           .ToListAsync();
        }
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryByDoctorIdAsync(int doctorId)
        {
            return _context.Set<MedicalHistoryModel>()
                           .Where(mh => mh.DoctorId == doctorId)
                           .ToListAsync();
        }
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryByDateAsync(DateTime date)
        {
            return _context.Set<MedicalHistoryModel>()
                           .Where(mh => mh.VisitDate.Date == date.Date)
                           .ToListAsync();
        }
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryBySymptomsAsync(string symptoms)
        {
            return _context.Set<MedicalHistoryModel>()
                           .Where(mh => mh.Symptoms.Contains(symptoms))
                           .ToListAsync();
        }
       
    }
}
