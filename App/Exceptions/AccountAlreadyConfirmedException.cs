namespace RekomBackend.App.Exceptions;

public class AccountAlreadyConfirmedException : Exception
{
   public AccountAlreadyConfirmedException(string? message = null) : base(message)
   {
   }
}