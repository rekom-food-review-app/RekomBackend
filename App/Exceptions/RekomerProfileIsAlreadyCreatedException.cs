namespace RekomBackend.App.Exceptions;

public class RekomerProfileIsAlreadyCreatedException : Exception
{
   public RekomerProfileIsAlreadyCreatedException(string? message = null) : base(message)
   {
   }
}