//using DanpheEMR.WEB.Iterface.Repository;
//using DanpheEMR.WEB.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DanpheEMR.WEB.Repository
//{
//    public class DoctorRepository : GenericRepository<DoctorModel>, IDoctorRepository
//    {
//        public DoctorRepository(DbContext context) : base(context)
//        {
//        }

//        public Task<List<DoctorModel>> FindDoctorsByNameAsync(string name)
//        {
//            return _context.Set<DoctorModel>()
//                           .Where(d => d.FullName.Contains(name))
//                           .ToListAsync();
//        }

//        public Task<List<DoctorModel>> GetDoctorsByDepartmentIdAsync(int departmentId)
//        {
//            return _context.Set<DoctorModel>()
//                           .Where(d => d.DepartmentId == departmentId)
//                           .ToListAsync();
//        }

//        public Task<List<DoctorModel>> GetDoctorsBySpecializationAsync(string specialization)
//        {
//            return _context.Set<DoctorModel>()
//                           .Where(d => d.Specialization.Contains(specialization))
//                           .ToListAsync();
//        }

//        public Task<List<DoctorModel>> GetDoctorsByAvailabilityAsync(bool isAvailable)
//        {
//            return _context.Set<DoctorModel>()
//                           .Where(d => d.IsAvailable == isAvailable)
//                           .ToListAsync();
//        }
//    }
//}