using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class UserDependencyException : Xeption
    {
        public UserDependencyException(Xeption innerException)
            : base(message: "User dependency error occurred, contact support",
                  innerException)
        { }
    }
}
