using DanpheEMR.WEB.Models;

namespace DanpheEMR.WEB.Iterface.Repository
{
    public interface IDoctorRepository : IGenericRepository<DoctorModel>
    {
       
        public Task<List<DoctorModel>> FindDoctorsByNameAsync(string name);
        public Task<List<DoctorModel>> GetDoctorsByDepartmentIdAsync(int departmentId);
        public Task<List<DoctorModel>> GetDoctorsBySpecializationAsync(string specialization);
        public Task<List<DoctorModel>> GetDoctorsByAvailabilityAsync(bool isAvailable);


    }
}
