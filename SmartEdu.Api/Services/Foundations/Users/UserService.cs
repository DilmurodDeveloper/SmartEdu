using SmartEdu.Api.Brokers.Loggings;
using SmartEdu.Api.Brokers.Storages;
using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

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

        public async ValueTask<User> ModifyUserAsync(User user)
        {
            try
            {
                ValidateAccountOnModify(user);

                User storageUser = 
                    await this.storageBroker.SelectUserByIdAsync(user.Id);

                ValidateStorageUser(storageUser, user.Id);

                return await this.storageBroker.UpdateUserAsync(user);
            }
            catch (NullUserException nullUserException)
            {
                var userValidationException =
                    new UserValidationException(nullUserException);
                
                this.loggingBroker.LogError(userValidationException);
                
                throw userValidationException;
            }
            catch (InvalidUserException invalidUserException)
            {
                var userValidationException =
                    new UserValidationException(invalidUserException);
                
                this.loggingBroker.LogError(userValidationException);
                
                throw userValidationException;
            }
            catch (NotFoundUserException notFoundUserException)
            {
                var userValidationException =
                    new UserValidationException(notFoundUserException);
                
                this.loggingBroker.LogError(userValidationException);
                
                throw userValidationException;
            }
        }
    }
}
