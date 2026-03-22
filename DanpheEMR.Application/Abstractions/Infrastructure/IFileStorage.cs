namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface IFileStorage
    {
        Task<string> UploadAsync(
            Stream file ,
            string fileName,
            string contentFile,
            CancellationToken cancellationToken = default);
        Task Deleteasync(
            string path,
            CancellationToken cancellationToken = default
            );
        Task<Stream> DowloadAsync(
            string path,
            CancellationToken cancellationToken = default);
        string GetAsync(string path);

    }
}
