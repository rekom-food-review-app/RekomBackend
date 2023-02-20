using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace RekomBackend.App.Helpers.s3;

public class S3Helper : IS3Helper
{
   private readonly string _bucketName;
   private readonly string _key;
   private readonly string _secretKey;
   private readonly RegionEndpoint _region;

   public S3Helper(IConfiguration configuration)
   {
      _bucketName = configuration.GetValue<string>("S3:BucketName")!;
      _key = configuration.GetValue<string>("S3:Key")!;
      _secretKey = configuration.GetValue<string>("S3:SecretKey")!;
      _region = RegionEndpoint.APNortheast1;
   }

   // public Task UploadOneFileAsync(IFormFile file, out string url)
   // {
   //    throw new NotImplementedException();
   // }

   public async Task<string> UploadOneFileAsync(IFormFile file)
   {
      var credentials = new BasicAWSCredentials(_key, _secretKey);
      var s3Config = new AmazonS3Config() { RegionEndpoint = _region };
      
      await using var memoryStr = new MemoryStream();
      await file.CopyToAsync(memoryStr);
      
      var uploadRequest = new TransferUtilityUploadRequest()
      {
         InputStream = memoryStr,
         Key = $"{Guid.NewGuid()}.{file.ContentType}",
         BucketName = _bucketName,
         CannedACL = S3CannedACL.NoACL
      };
      
      using var s3Client = new AmazonS3Client(credentials, s3Config);
      var transferUtility = new TransferUtility(s3Client);
      _ = transferUtility.UploadAsync(uploadRequest);

      return uploadRequest.Key;
   }

   // public async Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentials)
   // {
   //    var credentials = new BasicAWSCredentials(awsCredentials.Key, awsCredentials.SecretKey);
   //
   //    var config = new AmazonS3Config()
   //    {
   //       RegionEndpoint = Amazon.RegionEndpoint.APNortheast1
   //    };
   //
   //    var response = new S3ResponseDto();
   //
   //    try
   //    {
   //       var uploadRequest = new TransferUtilityUploadRequest()
   //       {
   //          InputStream = obj.InputStream,
   //          Key = obj.Name,
   //          BucketName = obj.BucketName,
   //          CannedACL = S3CannedACL.NoACL
   //       };
   //
   //       using var client = new AmazonS3Client(credentials, config);
   //
   //       var transferUtility = new TransferUtility(client);
   //
   //       await transferUtility.UploadAsync(uploadRequest);
   //
   //       // response.StatusCode = 200
   //    }
   //    catch (AmazonS3Exception e)
   //    {
   //       response.StatusCode = (int)e.StatusCode;
   //       response.Message = e.Message;
   //    }
   //    catch (Exception e)
   //    {
   //       response.StatusCode = 500;
   //       response.Message = e.Message;
   //    }
   //
   //    return response;
   // }
}