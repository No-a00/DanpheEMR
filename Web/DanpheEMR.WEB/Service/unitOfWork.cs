//using DanpheEMR.WEB.Iterface;
//using DanpheEMR.WEB.Iterface.Repository;
//using DanpheEMR.WEB.Models;
//using DanpheEMR.WEB.Repository;
//using Microsoft.EntityFrameworkCore;

//namespace DanpheEMR.WEB.Services 
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        private readonly DbContext _context;

//        //  Tên property phải trùng khớp hoàn toàn với Interface (Patients)
//        public IPatientRepository Patients { get; private set; }
//        public IDoctorRepository Doctors { get; private set; }
//        public IDepartmentRepository Departments { get; private set; }
//        public IMedicalHistoryRepository MedicalHistories { get; private set; }

//        //  Tiêm (Inject) DbContext và PatientRepo vào qua Constructor
//        public UnitOfWork(DbContext context, IPatientRepository patientRepo,IDoctorRepository doctorRepo,IDepartmentRepository departmentRepo ,IMedicalHistoryRepository medicalHistoryRepo)
//        {
//            _context = context;
//            Patients = patientRepo;
//            Doctors = doctorRepo;
//            Departments = departmentRepo;
//            MedicalHistories = medicalHistoryRepo;
//        }

//        public int SaveChanges()
//        {
//            return _context.SaveChanges();
//        }

//        public void Dispose()
//        {
//            _context?.Dispose();
//        }
//    }
//}