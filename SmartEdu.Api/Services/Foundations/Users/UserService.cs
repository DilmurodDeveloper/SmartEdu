using SmartEdu.Api.Brokers.Loggings;
using SmartEdu.Api.Brokers.Storages;
using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

namespace SmartEdu.Api.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<User> AddUserAsync(User user)
        {
            try
            {
                if (user is null)
                {
                    throw new NullUserException();
                }

                return await this.storageBroker.InsertUserAsync(user);
            }
            catch (NullUserException nullUserException)
            {
                var userValidationException =
                    new UserValidationException(nullUserException);

                this.loggingBroker.LogError(userValidationException);

                throw userValidationException;
            }
        }
    }
}
