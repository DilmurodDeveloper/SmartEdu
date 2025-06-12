using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class UserServiceException : Xeption
    {
        public UserServiceException(Xeption innerException)
            : base(message: "User service error occurred, contact support",
                  innerException)
        { }
    }
}
