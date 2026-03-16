using DanpheEMR.WEB.Models;

namespace DanpheEMR.WEB.Iterface.Repository
{
    public interface IDepartmentRepository : IGenericRepository<DepartmentModel>
    {
        public DepartmentModel GetDepartmentByName(string name);
        public Task<List<DoctorModel>> GetDocterInDepartmentAsync(string department);
        public Task<List<PatientModel>> GetPatientInDepartmentAsync(string department);


    }
}
