using Dropbox.Api;
using Dropbox.Api.Files;

using HiringSystem.Application.Common.Interfaces.Storage;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HiringSystem.Infrastructure.Storage;

public class Dropbox : IDropbox
{
    private readonly DropboxSettings _settings;
    private readonly DropboxClient _client;
    
    public Dropbox(IOptions<DropboxSettings> settings)
    {
        _settings = settings.Value;
        _client = new DropboxClient(_settings.AppKey);
    }
    
    public async Task<string> UploadAsync(string fileName, IFormFile file)
    {
        await using var fileStream = file.OpenReadStream();

        var path = $"/Apps/{_settings.Folder}/{fileName}";
        
        await _client.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: fileStream);
        
        var sharedLink = await _client.Sharing.CreateSharedLinkWithSettingsAsync(path);

        return sharedLink.Url;
    }

    public Task<Stream> DownloadAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListAsync()
    {
        throw new NotImplementedException();
    }
}