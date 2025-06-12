using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class NullUserException : Xeption
    {
        public NullUserException()
            : base(message: "User is null")
        { }
    }
}
