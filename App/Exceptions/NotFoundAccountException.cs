namespace RekomBackend.App.Exceptions;

public class NotFoundAccountException : Exception
{
   public NotFoundAccountException(string? message = null) : base(message) { }
}