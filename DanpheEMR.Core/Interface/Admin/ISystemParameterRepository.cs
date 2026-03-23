using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface ISystemParameterRepository
    {
        //  Admin
        Task<IEnumerable<SystemParameter>> GetAllAsync();

        Task<SystemParameter> GetByIdAsync(Guid Id);

        Task UpdateAsync(SystemParameter parameter);

       
        // Lấy chính xác nguyên cái Object Parameter dựa vào Tên (Key)
        Task<SystemParameter> GetByNameAsync(string parameterName);
    }
}