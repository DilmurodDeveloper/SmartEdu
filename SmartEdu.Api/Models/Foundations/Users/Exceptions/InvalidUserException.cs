using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class InvalidUserException : Xeption
    {
        public InvalidUserException()
            : base(message: "User is invalid. Please fix the errors and try again")
        { }

    }
}
