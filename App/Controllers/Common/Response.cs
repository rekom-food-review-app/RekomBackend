namespace RekomBackend.App.Controllers.Common;

public class Response<T>
{
   public string Code { get; set; } = null!;
   
   public string Message { get; set; } = null!;

   public T? Result { get; set; }
}