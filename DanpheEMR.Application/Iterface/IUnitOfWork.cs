using DanpheEMR.WEB.Iterface.Repository;
using DanpheEMR.WEB.Models;

namespace DanpheEMR.WEB.Iterface
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        IPatientRepository Patients { get; }
        IDoctorRepository Doctors { get; }
        IDepartmentRepository Departments { get; }
        IMedicalHistoryRepository MedicalHistories { get; }
    }
}
