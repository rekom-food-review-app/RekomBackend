using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace RekomBackend.App.Helpers;

public class S3Helper : IS3Helper
{
   private readonly string _bucketName;
   private readonly string _key;
   private readonly string _secretKey;
   private readonly RegionEndpoint _region;
   private readonly string _baseUrl;

   public S3Helper(IConfiguration configuration)
   {
      _bucketName = configuration.GetValue<string>("S3:BucketName")!;
      _key = configuration.GetValue<string>("S3:Key")!;
      _secretKey = configuration.GetValue<string>("S3:SecretKey")!;
      _region = RegionEndpoint.APNortheast1;
      _baseUrl = "https://rekom-bucket.s3.ap-northeast-1.amazonaws.com";
   }

   public async Task<string> UploadOneFileAsync(IFormFile file)
   {
      var imgPath = $"{Guid.NewGuid()}.{Path.GetExtension(file.Name)}";
      
      var credentials = new BasicAWSCredentials(_key, _secretKey);
      var s3Config = new AmazonS3Config() { RegionEndpoint = _region };
   
      await using var memoryStr = new MemoryStream();
      await file.CopyToAsync(memoryStr);
   
      var uploadRequest = new TransferUtilityUploadRequest()
      {
         InputStream = memoryStr,
         Key = imgPath,
         BucketName = _bucketName,
         CannedACL = S3CannedACL.NoACL
      };
   
      using var s3Client = new AmazonS3Client(credentials, s3Config);
      var transferUtility = new TransferUtility(s3Client);

      await transferUtility.UploadAsync(uploadRequest);

      return _baseUrl + "/" + imgPath;
   }

   public async Task<List<string>> UploadManyFileAsync(List<IFormFile> fileList)
   {
      var uploadTaskList = fileList.Select(file => Task.Run(async () => await UploadOneFileAsync(file))).ToList();

      await Task.WhenAll(uploadTaskList);

      return uploadTaskList.Select(t => t.Result).ToList();
   }
}