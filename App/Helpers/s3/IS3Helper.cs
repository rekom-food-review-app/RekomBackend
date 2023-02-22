namespace RekomBackend.App.Helpers;

public interface IS3Helper
{
   public Task<string> UploadOneFileAsync(IFormFile file);
}