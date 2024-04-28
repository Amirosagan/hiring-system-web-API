using Microsoft.AspNetCore.Http;

namespace HiringSystem.Application.Common.Interfaces.Storage;

public interface IDropbox
{
    Task<string> UploadAsync(string fileName, IFormFile file);
    Task<Stream> DownloadAsync(string fileName);
    Task DeleteAsync(string fileName);
    Task<IEnumerable<string>> ListAsync();
}