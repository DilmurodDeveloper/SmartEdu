using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<User> InsertUserAsync(User user);
    }
}
