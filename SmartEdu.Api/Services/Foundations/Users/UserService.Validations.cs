using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

namespace SmartEdu.Api.Services.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateUserNotNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }
    }
}
