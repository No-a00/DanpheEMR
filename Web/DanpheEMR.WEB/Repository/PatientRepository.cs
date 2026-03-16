//using DanpheEMR.WEB.Iterface;
//using DanpheEMR.WEB.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DanpheEMR.WEB.Repository
//{
//    public class PatientRepository : GenericRepository<PatientModel>, IPatientRepository
//    {

//        public PatientRepository(DbContext context) : base(context)
//        {
//        }


//        public async Task<List<PatientModel>> FindPatientsByNameAsync(string name)
//        {
//            return await _context.Set<PatientModel>()
//                                 .Where(p => p.FullName.Contains(name))
//                                 .ToListAsync();
//        }


//        public async Task<PatientModel> GetPatientsByDoctorIdAsync(int doctorId)
//        {

//            return await _context.Set<PatientModel>()
//                                 .FirstOrDefaultAsync(p => p.AssignedDoctorId == doctorId);
//        }
//    }
//}