namespace RekomBackend.App.Exceptions;

public class InvalidAccessTokenException : Exception
{
   public InvalidAccessTokenException(string? message = null) : base(message)
   {
   }
}