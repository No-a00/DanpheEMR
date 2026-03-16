//using DanpheEMR.WEB.Iterface.Repository;
//using DanpheEMR.WEB.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DanpheEMR.WEB.Repository

//{
//    public class DepartmentRepository : GenericRepository<DepartmentModel>, IDepartmentRepository
//    {
//        public DepartmentRepository(DbContext context) : base(context)
//        {
//        }
//        public DepartmentModel GetDepartmentByName(string name)
//        {
//            return _context.Set<DepartmentModel>().FindAsync(name).Result;
//        }
//        public Task<List<DoctorModel>> GetDocterInDepartmentAsync(string department)
//        {
//            return _context.Set<DoctorModel>()
//                           .Where(d => d.Department.ToString() == department)
//                           .ToListAsync();
//        }
//        public Task<List<PatientModel>> GetPatientInDepartmentAsync(string department)
//        {
//            return _context.Set<PatientModel>()
//                           .Where(p => p.Department.ToString() == department)
//                           .ToListAsync();
//        }
//    }
//}
