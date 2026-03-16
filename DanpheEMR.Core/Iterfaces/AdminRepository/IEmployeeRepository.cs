using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Domain.Admin
{
    public interface IEmployeeRepository
    {
       
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);

        Task DeactivateAsync(int id);

        // Các hàm nghiệp vụ đặc thù
        Task<IEnumerable<Employee>> SearchByNameOrContactAsync(string keyword);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId);
        Task<Employee> GetEmployeeByUserIdAsync(int userId);
        Task<Employee> GetEmployeeWithDetailsAsync(int id);
    }
}