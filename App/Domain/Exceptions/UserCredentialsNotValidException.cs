namespace Domain.Exceptions
{
    public class UserCredentialsNotValidException : Exception
    {
        public UserCredentialsNotValidException(string message) : base(message)
        {

        }
    }
}
