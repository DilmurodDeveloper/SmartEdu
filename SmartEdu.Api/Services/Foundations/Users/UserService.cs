using SmartEdu.Api.Brokers.Loggings;
using SmartEdu.Api.Brokers.Storages;
using SmartEdu.Api.Models.Foundations.Users;

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

        public ValueTask<User> AddUserAsync(User user) =>
            throw new NotImplementedException();
    }
}
