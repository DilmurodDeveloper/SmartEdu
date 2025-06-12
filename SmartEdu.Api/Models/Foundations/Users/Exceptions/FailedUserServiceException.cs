using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class FailedUserServiceException : Xeption
    {
        public FailedUserServiceException(Exception innerException)
            : base(message: "User service error occurred, contact support",
                  innerException)
        { }
    }
}
