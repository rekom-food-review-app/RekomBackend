namespace RekomBackend.App.Exceptions;

public class NotFoundRekomerProfileException : Exception
{
   public NotFoundRekomerProfileException(string? message = null!) : base(message)
   {
   }
}