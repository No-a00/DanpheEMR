//namespace DanpheEMR.WEB.Service;
//using DanpheEMR.WEB.Iterface;
//using DanpheEMR.WEB.Models;

//public class PatientRepository : IPatientRepo
//{
//    private readonly IUnitOfWork _unitOfWork;
//    Dbcontext _context;

//    public PatientRepository(DbContext context)
//    {
//        _unitOfWork = unitOfWork;
//        _context = _unitOfWork.context;
//    }
//    public async Task<List<PatientModel>> GetAllPatientsAsync()
//    {
//        try
//        {
//            return await _patientRepository.GetAllPatientsAsync();
//        }
//        catch (Exception)
//        {

//            throw;
//        }
        
//    }
//    public async Task<PatientModel> GetPatientByIdAsync(int id)
//    {
//        var partient =  await _unitOfWork.Patient.GetByIdAsync(id);
//    }
//    public async Task AddPatientAsync(PatientModel patient)
//    {
//        await _patientRepository.AddPatientAsync(patient);
//        _unitOfWork.SaveChanges();
//    }
//    public void UpdatePatient(PatientModel patient)
//    {
//        _patientRepository.UpdatePatient(patient);
//        _unitOfWork.SaveChanges();
//    }
//    public void DeletePatient(PatientModel patient)
//    {
//        _patientRepository.DeletePatient(patient);
//        _unitOfWork.SaveChanges();
//    }
//}

