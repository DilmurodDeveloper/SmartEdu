using Xeptions;

namespace SmartEdu.Api.Models.Foundations.Users.Exceptions
{
    public class NotFoundUserException : Xeption
    {
        public NotFoundUserException(Guid userId)
            : base(message: $"Couldn't find user with ID: {userId}")
        { }
    }
}
