using DanpheEMR.Core.Interface.Admin;

namespace DanpheEMR.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        //Admin 
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }



    }
}
