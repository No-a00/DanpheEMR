using DanpheEMR.Core.Iterfaces;


namespace DanpheEMR.Core.Iterface
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
       
    }
}
