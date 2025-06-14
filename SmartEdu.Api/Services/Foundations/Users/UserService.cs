using SmartEdu.Api.Brokers.Loggings;
using SmartEdu.Api.Brokers.Storages;
using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Services.Foundations.Users
{
    public partial class UserService : IUserService
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
        TryCatch(async () =>
        {
            ValidateUserOnAdd(user);

            return await this.storageBroker.InsertUserAsync(user);
        });

        public IQueryable<User> RetrieveAllUsers() =>
            TryCatch(() => this.storageBroker.SelectAllUsers());

        public ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
        TryCatch(async () =>
        {
            ValidateUserId(userId);

            User storageUser =
                await this.storageBroker.SelectUserByIdAsync(userId);

            ValidateStorageUser(storageUser, userId);

            return storageUser;
        });

        public ValueTask<User> ModifyUserAsync(User user) =>
        TryCatch(async () =>
        {
            ValidateAccountOnModify(user);

            User storageUser =
                await this.storageBroker.SelectUserByIdAsync(user.Id);

            ValidateStorageUser(storageUser, user.Id);

            return await this.storageBroker.UpdateUserAsync(user);
        });

        public async ValueTask<User> RemoveUserByIdAsync(Guid userId)
        {
            User user = await this.storageBroker.SelectUserByIdAsync(userId);
            return await this.storageBroker.DeleteUserAsync(user);
        }
    }
}