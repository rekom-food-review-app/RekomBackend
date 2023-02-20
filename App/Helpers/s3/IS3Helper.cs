namespace RekomBackend.App.Helpers.s3;

public interface IS3Helper
{
   public Task<string> UploadOneFileAsync(IFormFile file);
}