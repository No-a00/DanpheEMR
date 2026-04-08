namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}