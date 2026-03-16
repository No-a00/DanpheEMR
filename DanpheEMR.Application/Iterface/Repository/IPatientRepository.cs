// File: Web/DanpheEMR.WEB/Iterface/IPatientService.cs
namespace DanpheEMR.WEB.Iterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using DanpheEMR.WEB.Models;

public interface IPatientRepository : IGenericRepository<PatientModel> 
{  
    Task<List<PatientModel>> FindPatientsByNameAsync(string name); // Tìm theo ý
    Task<PatientModel> GetPatientsByDoctorIdAsync(int doctorId); // Tìm theo ý
}