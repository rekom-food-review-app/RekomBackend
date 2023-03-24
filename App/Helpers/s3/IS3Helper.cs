namespace RekomBackend.App.Helpers;

public interface IS3Helper
{
   public Task<string> UploadOneFileAsync(IFormFile file);

   public Task<List<string>> UploadManyFileAsync(List<IFormFile> fileList);
}