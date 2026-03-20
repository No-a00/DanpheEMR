using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interface.Admin
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> SearchByNameOrContactAsync(string keyword);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId);
        Task<Employee> GetEmployeeByUserIdAsync(int userId);
        Task<Employee> GetEmployeeWithDetailsAsync(int id);
    }
}