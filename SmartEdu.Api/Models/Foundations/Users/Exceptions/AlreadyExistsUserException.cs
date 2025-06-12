using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class AlreadyExistsUserException : Xeption
    {
        public AlreadyExistsUserException(Exception innerException)
            : base(message: "User already exists", innerException)
        { }
    }
}
