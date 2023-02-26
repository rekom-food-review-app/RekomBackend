namespace RekomBackend.App.Exceptions;

public class NotFoundRekomerException : Exception
{
   public NotFoundRekomerException(string? message = null!) : base(message)
   {
   }
}